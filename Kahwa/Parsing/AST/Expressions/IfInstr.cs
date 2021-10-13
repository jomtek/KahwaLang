using Kahwa.Lexing;
using Kahwa.Parsing.AST.Statements;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.AST.Expressions
{
    public class IfInstr : IExpr
    {
        public readonly ExprNode Condition;
        public Block MainBranch;
        public Block ElseBranch;

        public IfInstr(ExprNode condition, Block mainBranch, Block elseBranch)
        {
            Condition = condition;
            MainBranch = mainBranch;
            ElseBranch = elseBranch;
        }

        public static IfInstr Consume(Parser parser)
        {
            parser.TryEat(TokenType.IF);

            Block mainBranch = null;
            Block elseBranch = null;
            var elifBranches = new List<IfInstr>();

            #region Base Condition
            ExprNode baseCondition;
            try
            {
                baseCondition = parser.TryConsumer(ExprNode.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected expression after IF token"),
                    parser.Cursor
                );
            }
            #endregion

            #region Base Branch
            InstrNode mainInstr;
            try
            {
                mainInstr = parser.TryConsumer(InstrNode.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected block or instruction after IF condition"),
                    parser.Cursor
                );
            }

            mainBranch = Utils.InstrToBlock(mainInstr);
            #endregion

            #region Collect ELIF Branches
            while (true)
            {
                ExprNode condition;
                Block branch;

                parser.SkipThroughEOLs();

                try
                {
                    parser.TryEat(TokenType.ELIF);
                }
                catch (ParserException)
                {
                    break;
                }

                try
                {
                    condition = parser.TryConsumer(ExprNode.Consume);
                }
                catch (ParserException ex)
                {
                    if (!ex.IsExceptionFictive()) throw;
                    throw new ParserException(
                        new ExpectedElementException("Expected expression after ELIF token"),
                        parser.Cursor
                    );
                }

                try
                {
                    // For each ELIF branch, we want a handsome block instead of an instruction
                    branch = parser.TryConsumer((Parser p) => Block.Consume(p));
                }
                catch (ParserException ex)
                {
                    if (!ex.IsExceptionFictive()) throw;
                    throw new ParserException(
                        new ExpectedElementException("Expected block or instruction after ELIF condition"),
                        parser.Cursor
                    );
                }

                elifBranches.Add(new IfInstr(condition, branch, null));
            }

            #endregion

            #region Collect ELSE Branch
            parser.SkipThroughEOLs();

            bool isThereElse = true;
            try
            {
                parser.TryEat(TokenType.ELSE);
            }
            catch (ParserException)
            {
                isThereElse = false;
            }

            if (isThereElse)
            {
                InstrNode elseInstr;

                try
                {
                    elseInstr = parser.TryConsumer(InstrNode.Consume);
                }
                catch (ParserException ex)
                {
                    if (!ex.IsExceptionFictive()) throw;
                    throw new ParserException(
                        new ExpectedElementException("Expected block or instruction after ELSE token"),
                        parser.Cursor
                    );
                }

                elseBranch = Utils.InstrToBlock(elseInstr);

                if (elifBranches.Count > 0)
                    elifBranches[^1].ElseBranch = elseBranch;
            }
            #endregion

            #region Merge ELIF Instructions
            if (elifBranches.Count > 0)
            {
                elifBranches.Reverse();
                for (int i = 0; i < elifBranches.Count - 1; i++)
                {
                    IfInstr branch = elifBranches[i];
                    elifBranches[i + 1].ElseBranch = new Block(new InstrNode[]
                    {
                        new InstrNode(new ExprInstr(branch), new CodePosition())
                    });
                }
            }

            if (elifBranches.Count > 0)
            {
                IfInstr lastElifBranch = elifBranches[^1];
                elseBranch = new Block(new InstrNode[]
                {
                    new InstrNode(new ExprInstr(lastElifBranch), new CodePosition())
                });
            }
            #endregion

            return new IfInstr(baseCondition, mainBranch, elseBranch);
        }
    }
}
