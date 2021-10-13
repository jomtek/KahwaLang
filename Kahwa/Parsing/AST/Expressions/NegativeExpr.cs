using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Expressions
{
    public class NegativeExpr : IExpr
    {
        public readonly IExpr Value;

        public NegativeExpr(IExpr value)
        {
            Value = value;
        }

        public static IExpr Consume(Parser parser)
        {
            Token prefix = parser.TryManyEats(new TokenType[] { TokenType.PLUS, TokenType.MINUS });

            IExpr expression;
            try
            {
                expression = parser.TryConsumer(ExprNode.Consume).Value;
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected expression after PLUS or MINUS prefix"),
                    parser.Cursor
                );
            }

            if (prefix.Type == TokenType.MINUS)
            {
                return new NegativeExpr(expression);
            }
            else
            {
                return expression;
            }
        }
    }
}
