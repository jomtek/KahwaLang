using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation
{
    public class GenerationException : Exception
    {
        public readonly CodePosition Position;

        public GenerationException(string message, CodePosition pos) : base(message)
        {
            Position = pos;
        }
    }
}
