using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Expressions
{
    public class InfixOp : IExpr
    {
        public readonly IExpr LeftOperand;
        public readonly IExpr RightOperand;
        public readonly Token Operator;

        public InfixOp(IExpr left, IExpr right, Token op)
        {
            LeftOperand = left;
            RightOperand = right;
            Operator = op;
        }
    }
}
