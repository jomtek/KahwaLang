using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Statements.Loops
{
    public class WhileLoop : IInstr
    {
        public readonly ExprNode Condition;
        public readonly Block Block;

        public WhileLoop(ExprNode condition, Block block)
        {
            Condition = condition;
            Block = block;
        }

        public static WhileLoop Consume(Parser parser)
        {
            parser.TryEat(TokenType.WHILE);

            ExprNode condition;
            try
            {
                condition = parser.TryConsumer(ExprNode.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected condition after WHILE token"),
                    parser.Cursor
                );
            }

            InstrNode instr;
            try
            {
                instr = parser.TryConsumer(InstrNode.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected instruction after expression in WHILE loop"),
                    parser.Cursor
                );
            }

            return new WhileLoop(condition, Utils.InstrToBlock(instr));
        }
    }
}