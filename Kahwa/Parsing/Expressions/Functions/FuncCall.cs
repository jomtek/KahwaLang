using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Expressions.Functions
{
    public class FuncCall : IExpr
    {
        public readonly Name Name;
        public readonly ExprNode[] Arguments;

        public FuncCall(Name name, ExprNode[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public static FuncCall Consume(Parser parser)
        {
            Name name;
            ExprNode[] arguments;

            name = parser.TryConsumer(Name.Consume); // foo

            parser.TryEat(TokenType.L_PAREN); // (

            arguments = Utils.ParseSequence(parser, ExprNode.Consume); // baz, baz, baz

            parser.TryEat(TokenType.R_PAREN, false); // )

            return new FuncCall(name, arguments);
        }
    }
}
