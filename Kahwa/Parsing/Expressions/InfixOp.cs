using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Expressions
{
    public class InfixOp : IExpr
    {
        public readonly ExprNode LeftOperand;
        public readonly ExprNode RightOperand;
        public readonly Token Operator;

        public InfixOp(ExprNode left, ExprNode right, Token op)
        {
            LeftOperand = left;
            RightOperand = right;
            Operator = op;
        }
    }
}
