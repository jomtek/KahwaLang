using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Expressions.Lists
{
    public class NativeList : IExpr
    {
        public readonly ExprNode[] Items;

        public NativeList(ExprNode[] items)
        {
            Items = items;
        }

        public static NativeList Consume(Parser parser)
        {
            parser.TryEat(TokenType.L_BRACKET);

            ExprNode[] items = Utils.ParseSequence(parser, ExprNode.Consume);
            
            parser.TryEat(TokenType.R_BRACKET, false);

            return new NativeList(items);
        }
    }
}
