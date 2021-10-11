using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Lexing
{
    public struct CodePosition
    {
        public readonly int Line;
        public readonly int Column;

        public CodePosition(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public string Pretty()
        {
            return $"line: {Line}, column: {Column}";
        }
    }
}
