using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Statements
{
    public class ExprInstr : IInstr
    {
        public readonly IExpr Expression;
        
        public ExprInstr(IExpr expression)
        {
            Expression = expression;
        }

        public static ExprInstr Consume(Parser parser)
        {
            IExpr expression = parser.TryConsumer(ExprNode.Consume).Value;
            return new ExprInstr(expression);
        }
    }
}
