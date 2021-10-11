using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Lexing
{
    public class Token
    {
        public readonly string Value;
        public readonly TokenType Type;
        public readonly CodePosition Pos;

        public Token(string value, TokenType type, CodePosition pos)
        {
            Value = value;
            Type = type;
            Pos = pos;
        }

        public override string ToString()
        {
            if (Type != TokenType.EOL)
            {
                return $"{Type} - {Pos.Line}: {Pos.Column} - value: {Value}";
            }
            else
            {
                return "EOL";
            }
        }
    }
}
