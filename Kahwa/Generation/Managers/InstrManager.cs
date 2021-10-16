using Kahwa.Lexing;
using Kahwa.Parsing.AST;
using Kahwa.Parsing.AST.Statements;
using System;

namespace Kahwa.Generation.Managers
{
    internal static class InstrManager
    {
        private static void Manage(Mutation mut, BCGenerator generator)
        {
            switch (mut.MutationOp)
            {
                case TokenType.ASSIGN:
                    generator.Cursor = mut.NewValue.Pos;
                    ExprManager.Manage(mut.NewValue.Value, generator);

                    generator.Cursor = mut.Name.Identifier.Pos;
                    var identifier = generator.SymbolTable.GetIndex(mut.Name.Identifier.Value);

                    var instruction = new BCInstruction(OpCode.STORE_FAST, identifier);
                    generator.Instructions.Add(instruction);

                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public static void Manage(IInstr instr, BCGenerator generator)
        {
            switch (instr)
            {
                case Mutation mut:
                    Manage(mut, generator);
                    break;
                default:
                    break;
            }
        }
    }
}
