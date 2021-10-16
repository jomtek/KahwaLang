using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Expressions
{
    public class Block : IExpr
    {
        public readonly InstrNode[] Instructions;

        public Block(InstrNode[] instructions)
        {
            Instructions = instructions;
        }

        public static Block Consume(Parser parser, bool topLevel = false)
        {
            parser.SkipThroughNewlines();

            if (!topLevel)
                parser.TryEat(TokenType.L_CURLY_BRACKET);

            InstrNode[] statements =
                parser.TryConsumer((Parser p) => ParseStatementSeq(parser, topLevel));

            if (!topLevel)
                parser.TryEat(TokenType.R_CURLY_BRACKET, false);

            return new Block(statements);
        }

        private static InstrNode[] ParseStatementSeq(Parser parser, bool topLevel)
        {
            var statements = new List<InstrNode>();
            bool isLastEOL = true;

            while (parser.CanLookAhead())
            {
                InstrNode statement = null;
                bool eolFailed = false;

                try
                {
                    parser.TryManyEats(new TokenType[] { TokenType.EOL, TokenType.SEMICOLON });
                }
                catch (ParserException ex)
                {
                    eolFailed = true;
                }

                if (eolFailed)
                {
                    if (!isLastEOL)
                    {
                        TokenType nextTokenType;
                        try
                        {
                            nextTokenType = parser.LookAhead().Type;
                        }
                        catch (ParserException) // EOF
                        {
                            break;
                        }

                        if (nextTokenType == TokenType.R_CURLY_BRACKET)
                            break;

                        throw new ParserException(
                            new UnexpectedTokenException(nextTokenType),
                            parser.LookAhead().Pos
                        );
                    }

                    try
                    {
                        statement = parser.TryConsumer(InstrNode.Consume);
                    }
                    catch (ParserException ex)
                    {
                        if (!ex.IsExceptionFictive()) throw;
                        if (!topLevel && parser.LookAhead().Type != TokenType.R_CURLY_BRACKET)
                        {
                            throw new ParserException(
                                new UnexpectedTokenException(parser.LookAhead().Type),
                                parser.Cursor
                            );
                        }
                    }

                    statements.Add(statement);
                    isLastEOL = false;
                }
                else
                {
                    isLastEOL = true;
                }
            }

            return statements.ToArray();
        }
    }
}
