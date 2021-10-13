using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Types
{
    public interface IType
    { }

    public class TypeNode
    {
        public readonly TokenType Value;
        public readonly CodePosition Pos;

        public TypeNode(TokenType value, CodePosition pos)
        {
            Value = value;
            Pos = pos;
        }

        public static TypeNode Consume(Parser parser)
        {
            var oldCursor = parser.Cursor;
            var value = parser.TryManyEats(TokenInformation.PrimitiveTypes).Type;
            return new TypeNode(value, oldCursor);
        }
    }
}
