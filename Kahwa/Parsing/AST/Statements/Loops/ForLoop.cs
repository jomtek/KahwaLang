using Kahwa.Lexing;
using Kahwa.Parsing.AST.Expressions;
using Kahwa.Parsing.Exceptions;

namespace Kahwa.Parsing.AST.Statements.Loops
{
    public class ForLoop : IInstr
    {
        public readonly Token Index;
        public readonly ExprNode Array;
        public readonly Block Block;

        public ForLoop(Token index, ExprNode array, Block block)
        {
            Index = index;
            Array = array;
            Block = block;
        }

        public static ForLoop Consume(Parser parser)
        {
            parser.TryEat(TokenType.FOR);
            Token id;
            ExprNode array;

            id = parser.TryEat(TokenType.IDENTIFIER, false);

            parser.TryEat(TokenType.IN, false);

            try
            {
                array = parser.TryConsumer(ExprNode.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected expression after IN token"),
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
                    new ExpectedElementException("Expected instruction after expression in FOR loop"),
                    parser.Cursor
                );
            }

            return new ForLoop(id, array, Utils.InstrToBlock(instr));
        }

    }
}