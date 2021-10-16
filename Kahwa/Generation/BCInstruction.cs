using Kahwa.Generation.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation
{
    internal class BCInstruction
    {
        public readonly OpCode OpCode;
        public readonly int Argument;

        public BCInstruction(OpCode opcode, int argument)
        {
            OpCode = opcode;
            Argument = argument;
        }

        public override string ToString()
        {
            // LOAD_CONST   1
            // STORE_FAST   2
            // BINARY_ADD   (-1)

            if (Argument != -1)
            {
                return $"{Enum.GetName(OpCode)}  {Argument}";
            }
            else
            {
                return $"{Enum.GetName(OpCode)}";
            }
        }

        public string ToString(SymbolTable symbols, ConstantTable constants)
        {
            string annotation = null;
            string type = null;

            switch (OpCode)
            {
                case OpCode.LOAD_CONST:
                    var constant = constants.GetValue(Argument).Token;
                    annotation = constant.Value;
                    type = Enum.GetName(constant.Type).ToLower();
                    break;
                case OpCode.LOAD_FAST:
                case OpCode.STORE_FAST:
                    annotation = symbols.GetValue(Argument);
                    type = "iden";
                    break;
                case OpCode.COMPARE_OP:
                    type = Enum.GetName(GenerationInformation.ComparisonOperators[Argument]).ToLower();
                    break;
            }

            if (type == null)
            {
                return ToString();
            }
            else
            {
                return $"{ToString()}\t\t{type} {annotation}";
            }
        }
    }
}
