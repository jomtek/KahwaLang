using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Statements
{
    public class Mutation : IInstr
    {
        public readonly Name Name;
        public readonly TokenType MutationOp;
        public readonly ExprNode NewValue;

        public Mutation(TokenType mutationOp, Name name, ExprNode newValue)
        {
            Name = name;
            MutationOp = mutationOp;
            NewValue = newValue;
        }

        public static Mutation Consume(Parser parser)
        {
            Name name = null;
            TokenType mutationOp;
            ExprNode newValue = null;

            var oldCursor = parser.Cursor;

            name = parser.TryConsumer(Name.Consume);
            mutationOp = parser.TryManyEats(TokenInformation.MutationOperators).Type;

            if (mutationOp != TokenType.ASSIGN)
            {
                throw new ParserException(
                    new UnexpectedElementException("Unexpected explicit type restriction"),
                    oldCursor
                );
            }

            try
            {
                newValue = parser.TryConsumer(ExprNode.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected expression after mutation operator"),
                    parser.Cursor
                );
            }

            return new Mutation(mutationOp, name, newValue);
        }
    }
}
