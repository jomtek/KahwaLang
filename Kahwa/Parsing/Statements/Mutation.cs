using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Statements
{
    public class Mutation : IInstr
    {
        public readonly Token BaseVariable;
        public readonly TokenType MutationOp;
        public readonly ExprNode NewValue;

        public Mutation(TokenType mutationOp, Token baseVariable, ExprNode newValue)
        {
            BaseVariable = baseVariable;
            MutationOp = mutationOp;
            NewValue = newValue;
        }

        public static Mutation Consume(Parser parser)
        {
            Token baseVariable = null;
            TokenType mutationOp;
            ExprNode newValue = null;

            baseVariable = parser.TryEat(TokenType.IDENTIFIER);
            mutationOp = parser.TryManyEats(TokenInformation.MutationOperators).Type;

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

            return new Mutation(mutationOp, baseVariable, newValue);
        }
    }
}
