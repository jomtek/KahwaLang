using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation.Memory
{
    internal class SymbolTable : Table<string>
    {
        public void AssertExistence(string identifier, BCGenerator generator)
        {
            if (!_elements.Contains(identifier))
            {
                throw new GenerationException($"Unknown identifier `{identifier}`", generator.Cursor);
            }
        }
    }
}
