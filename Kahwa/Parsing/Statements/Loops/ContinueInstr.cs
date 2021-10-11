using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Statements.Loops
{
    public class ContinueInstr : IInstr
    {
        public static ContinueInstr Consume(Parser parser)
        {
            parser.TryEat(TokenType.CONTINUE);
            return new ContinueInstr();
        }
    }
}
