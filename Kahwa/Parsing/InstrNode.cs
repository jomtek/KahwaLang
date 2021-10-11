using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using Kahwa.Parsing.Expressions;
using Kahwa.Parsing.Statements;
using Kahwa.Parsing.Statements.Functions;
using Kahwa.Parsing.Statements.Loops;
using System;

namespace Kahwa.Parsing
{
    public interface IInstr
    { }

    public class InstrNode
    {
        public readonly IInstr Value;
        public readonly CodePosition Pos;

        public InstrNode(IInstr value, CodePosition pos)
        {
            Value = value;
            Pos = pos;
        }

        public static InstrNode Consume(Parser parser)
        {
            var oldCursor = parser.Cursor;

            IInstr instr;

            switch (parser.LookAhead().Type)
            {
                case TokenType.WHILE:
                    instr = parser.TryConsumer(WhileLoop.Consume);
                    break;

                case TokenType.FOR:
                    instr = parser.TryConsumer(ForLoop.Consume);
                    break;

                case TokenType.RETURN:
                    instr = parser.TryConsumer(ReturnInstr.Consume);
                    break;

                case TokenType.BREAK:
                    instr = parser.TryConsumer(BreakInstr.Consume);
                    break;

                case TokenType.CONTINUE:
                    instr = parser.TryConsumer(ContinueInstr.Consume);
                    break;

                default:
                    instr = parser.TryManyConsumers(new Func<Parser, IInstr>[] {
                        Mutation.Consume,
                        ExprInstr.Consume
                    });
                    break;
            }

            if (instr == null)
                throw new ParserException(new ConsumerFailed(), parser.Cursor);

            return new InstrNode(instr, oldCursor);
        }
    }
}
