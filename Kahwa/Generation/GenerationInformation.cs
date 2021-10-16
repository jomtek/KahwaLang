using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation
{
    internal static class GenerationInformation
    {
        public static List<TokenType> ComparisonOperators = new List<TokenType>()
        {
            TokenType.LESS,
            TokenType.LESS_EQ,
            TokenType.EQ,
            TokenType.NOT_EQ,
            TokenType.GREATER,
            TokenType.GREATER_EQ
        };
    }
}
