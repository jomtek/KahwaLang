using Kahwa.Lexing;
using Kahwa.Parsing.AST;
using Kahwa.Parsing.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation.Managers
{
    internal static class ExprManager
    {
        private static void Manage(Literal literal, BCGenerator generator)
        {
            OpCode opcode;
            int index;

            switch (literal.Token.Type)
            {
                case TokenType.IDENTIFIER:
                    generator.SymbolTable.AssertExistence(literal.Token.Value, generator);
                    index = generator.SymbolTable.GetIndex(literal.Token.Value);
                    opcode = OpCode.LOAD_FAST;
                    break;
                default:
                    index = generator.ConstantTable.GetIndex(literal);
                    opcode = OpCode.LOAD_CONST;
                    break;
            }

            var instruction = new BCInstruction(opcode, index);
            generator.Instructions.Add(instruction);
        }

        private static void Manage(InfixOp infixOp, BCGenerator generator)
        {
            Manage(infixOp.LeftOperand, generator);
            Manage(infixOp.RightOperand, generator);

            var dict = new Dictionary<TokenType, OpCode>()
            {
                [TokenType.PLUS]     = OpCode.BINARY_ADD,
                [TokenType.MINUS]    = OpCode.BINARY_SUBTRACT,
                [TokenType.MULTIPLY] = OpCode.BINARY_MULTIPLY,
                [TokenType.DIVIDE]   = OpCode.BINARY_TRUE_DIVIDE,
                [TokenType.POWER]    = OpCode.BINARY_POWER,
                [TokenType.MODULO]   = OpCode.BINARY_MODULO,
            };

            BCInstruction instruction;
            if (dict.ContainsKey(infixOp.Operator.Type))
            {
                instruction = new BCInstruction(dict[infixOp.Operator.Type], -1);
            }
            else
            {
                // LESS, GREATER, LESS_EQ...
                int opIndex = GenerationInformation.ComparisonOperators.IndexOf(infixOp.Operator.Type);
                instruction = new BCInstruction(OpCode.COMPARE_OP, opIndex);
            }

            generator.Instructions.Add(instruction);
        }

        private static void Manage(NotExpr notExpr, BCGenerator generator)
        {
            Manage(notExpr.Value, generator);
            var instruction = new BCInstruction(OpCode.UNARY_NOT, -1);
            generator.Instructions.Add(instruction);
        }

        private static void Manage(NegativeExpr negExpr, BCGenerator generator)
        {
            Manage(negExpr.Value, generator);
            var instruction = new BCInstruction(OpCode.UNARY_NEGATIVE, -1);
            generator.Instructions.Add(instruction);
        }

        public static void Manage(IExpr expr, BCGenerator generator)
        {
            switch (expr)
            {
                case Literal literal:
                    Manage(literal, generator);
                    break;
                case InfixOp infixOp:
                    Manage(infixOp, generator);
                    break;
                case NotExpr notExpr:
                    Manage(notExpr, generator);
                    break;
                case NegativeExpr negExpr:
                    Manage(negExpr, generator);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
