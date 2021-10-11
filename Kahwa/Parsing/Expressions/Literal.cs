using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Expressions
{
    public class Literal : IExpr
    {
        public readonly Token Token;
        
        public Literal(Token token)
        {
            Token = token;
        }

        public static Literal Consume(Parser parser)
        {
            Token token = parser.TryManyEats(TokenInformation.Literals);
            return new Literal(token);
        }
    }
}
