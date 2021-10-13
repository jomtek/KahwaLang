using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Expressions
{
    public class NativeArray : IExpr
    {
        public readonly ExprNode[] Items;

        public NativeArray(ExprNode[] items)
        {
            Items = items;
        }

        public static NativeArray Consume(Parser parser)
        {
            parser.TryEat(TokenType.L_BRACKET);

            ExprNode[] items = Utils.ParseSequence(parser, ExprNode.Consume);
            
            parser.TryEat(TokenType.R_BRACKET, false);

            return new NativeArray(items);
        }
    }
}
