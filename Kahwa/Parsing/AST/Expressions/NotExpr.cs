using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Expressions
{
    public class NotExpr : IExpr
    {
        public readonly IExpr Value;

        public NotExpr(IExpr value)
        {
            Value = value;
        }

        public static NotExpr Consume(Parser parser)
        {
            IExpr expr;

            parser.TryEat(TokenType.NOT);

            try
            {
                expr = parser.TryConsumer(ExprNode.Consume).Value;
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected expression after NOT token"),
                    parser.Cursor
                );
            }

            return new NotExpr(expr);
        }
    }
}
