using Kahwa.Lexing;
using Kahwa.Parsing.AST;
using Kahwa.Parsing.AST.Expressions;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;

namespace Kahwa.Parsing
{
    public static class Utils
    {
        public static Block InstrToBlock(InstrNode instr)
        {
            if (instr.Value is Block)
                return (Block)instr.Value;
            else
                return new Block(new InstrNode[] { instr });
        }

        public static T[] ParseSequence<T>(Parser parser, Func<Parser, T> consumer, TokenType delimiter = TokenType.COMMA)
        {
            var sequence = new List<T>();
            bool lastlyEaten = false;
            Token lastTokenEaten = null;

            while (true)
            {
                try
                {
                    sequence.Add(parser.TryConsumer(consumer));
                    lastlyEaten = false;
                    lastTokenEaten = parser.TryEat(delimiter);
                    lastlyEaten = true;
                }
                catch (ParserException ex)
                {
                    if (!ex.IsExceptionFictive()) throw;
                    break;
                }
            }

            if (lastlyEaten)
            {
                throw new ParserException(
                    new UnexpectedTokenException(delimiter),
                    lastTokenEaten.Pos
                );
            }

            return sequence.ToArray();
        }
    }
}
