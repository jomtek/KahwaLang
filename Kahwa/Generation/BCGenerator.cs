using Kahwa.Generation.Managers;
using Kahwa.Generation.Memory;
using Kahwa.Lexing;
using Kahwa.Parsing.AST;
using Kahwa.Parsing.AST.Expressions;
using Kahwa.Parsing.AST.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Generation
{
    public class BCGenerator
    {
        internal ConstantTable ConstantTable { get; set; }
        internal SymbolTable SymbolTable { get; set; }
        internal List<BCInstruction> Instructions { get; set; }

        internal CodePosition Cursor { get; set; }

        public BCGenerator(Block ast)
        {
            ConstantTable = new ConstantTable();
            SymbolTable = new SymbolTable();
            Instructions = new List<BCInstruction>();

            Cursor = new CodePosition(0, 0);

            foreach (InstrNode node in ast.Instructions)
            {
                Cursor = node.Pos;
                InstrManager.Manage(node.Value, this);
            }
        }

       
    }
}
