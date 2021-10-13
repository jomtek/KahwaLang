using Kahwa.Lexing;
using Kahwa.Parsing.Algorithms;
using Kahwa.Parsing.AST.Expressions;
using Kahwa.Parsing.AST.Expressions.Functions;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST
{
    public interface IExpr
    { }

    public class ExprNode
    {
        public readonly IExpr Value;
        public readonly CodePosition Pos;

        public ExprNode(IExpr value, CodePosition pos)
        {
            Value = value;
            Pos = pos;
        }

        public static ExprNode Consume(Parser parser)
        {
            CodePosition oldCursor = parser.Cursor;
            return new ExprNode(
                parser.TryConsumer((Parser p) => ParseBinOpSeq(p)),
                oldCursor
            );
        }

        private static IExpr ParseBinOpSeq(Parser parser, bool uniOpPrivilege = false)
        {
            var operands = new List<IExpr>();
            var operators = new List<Token>();

            while (true)
            {
                try
                {
                    operands.Add(ParseOperand(parser));
                    if (uniOpPrivilege) break;
                    operators.Add(ParseOperator(parser));
                }
                catch (ParserException ex)
                {
                    if (!ex.IsExceptionFictive())
                        throw;
                    else
                        break;
                }
            }

            if (operands.Count == 0)
            {
                throw new ParserException(new ConsumerFailed(), parser.Cursor);
            }
            else if (operators.Count > operands.Count - 1)
            {
                throw new ParserException(
                    new UnexpectedTokenException(operators.Last().Type),
                    operators.Last().Pos
                );
            }
            else if (operands.Count == 1)
            {
                return operands[0];
            }

            return ShuntingYard.Go(operands, operators);
        }

        private static Token ParseOperator(Parser parser)
        {
            if (!TokenInformation.AlgebraOperators.Contains(parser.LookAhead().Type))
                throw new ParserException(new ConsumerFailed(), parser.Cursor);
            return parser.TryManyEats(TokenInformation.AlgebraOperators);
        }

        private static IExpr ParseOperand(Parser parser)
        {
            IExpr operand = null;

            switch (parser.LookAhead().Type)
            {
                case TokenType.EOL:
                case TokenType.L_CURLY_BRACKET:
                    operand = parser.TryConsumer((Parser p) => Block.Consume(p));
                    break;

                case TokenType.L_PAREN:
                    operand = parser.TryConsumer(ParseParenthesisExpr);
                    break;

                case TokenType.IDENTIFIER:
                    operand = parser.TryManyConsumers(new Func<Parser, IExpr>[]
                    {
                        FuncCall.Consume,
                        Literal.Consume
                    });
                    break;

                case TokenType.DOUBLE_LIT:
                case TokenType.INTEGER_LIT:
                case TokenType.STRING_LIT:
                case TokenType.CHAR_LIT:
                case TokenType.BOOLEAN_LIT:
                    operand = parser.TryConsumer(Literal.Consume);
                    break;

                case TokenType.L_BRACKET:
                    operand = parser.TryConsumer(NativeArray.Consume);
                    break;
                
                case TokenType.NOT:
                    operand = parser.TryConsumer(NotExpr.Consume);
                    break;

                case TokenType.MINUS:
                case TokenType.PLUS:
                    operand = parser.TryConsumer(NegativeExpr.Consume);
                    break;

                case TokenType.IF:
                    operand = parser.TryConsumer(IfInstr.Consume);
                    break;

                case TokenType.FUN:
                    operand = parser.TryConsumer(FuncDecl.Consume);
                    break;
            }

            if (operand == null)
                throw new ParserException(new ConsumerFailed(), parser.Cursor);

            return operand;
        }

        private static IExpr ParseParenthesisExpr(Parser parser)
        {
            Token leftParenthesis = parser.TryEat(TokenType.L_PAREN);
            var expr = parser.TryConsumer(Consume).Value;

            try
            {
                parser.TryEat(TokenType.R_PAREN);
            }
            catch (ParserException)
            {
                throw new ParserException(
                    new ExpectedTokenException(TokenType.R_PAREN),
                    parser.Cursor
                );
            }

            return expr;
        }
    }
}
