using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation
{
    internal enum OpCode
    {
        LOAD_CONST,
        LOAD_FAST,
        STORE_FAST,

        BINARY_ADD,
        BINARY_SUBTRACT,
        BINARY_MULTIPLY,
        BINARY_TRUE_DIVIDE,
        BINARY_FLOOR_DIVIDE,
        BINARY_POWER,
        BINARY_MODULO,
        COMPARE_OP,

        UNARY_NOT,
        UNARY_NEGATIVE,

        NOP
    }
}
