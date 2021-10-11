using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Statements.Functions
{
    public class ReturnInstr : IInstr
    {
        public readonly ExprNode Value;

        public ReturnInstr(ExprNode value)
        {
            Value = value;
        }

        public static ReturnInstr Consume(Parser parser)
        {
            parser.TryEat(TokenType.RETURN);

            ExprNode value;
            try
            {
                value = parser.TryConsumer(ExprNode.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                value = null; // return;
            }

            return new ReturnInstr(value);
        }
    }
}