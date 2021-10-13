using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Statements.Loops
{
    public class BreakInstr : IInstr
    {
        public static BreakInstr Consume(Parser parser)
        {
            parser.TryEat(TokenType.BREAK);
            return new BreakInstr();
        }
    }
}
