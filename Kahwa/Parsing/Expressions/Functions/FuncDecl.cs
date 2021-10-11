using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using Kahwa.Parsing.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Expressions.Functions
{
    public class FuncDecl : IExpr
    {
        public readonly Name Name;
        public readonly Name[] Parameters;
        public readonly Block Block;

        public FuncDecl(Name name, Name[] parameters, Block block)
        {
            Name = name;
            Parameters = parameters;
            Block = block;
        }

        public static FuncDecl Consume(Parser parser)
        {
            // fun foo(a, b, c:String) {}
            Name name;
            Name[] parameters;
            Block block;

            parser.TryEat(TokenType.FUN);
            
            try
            {
                name = parser.TryConsumer(Name.Consume);
            }
            catch (ParserException ex)
            {
                if (!ex.IsExceptionFictive()) throw;
                throw new ParserException(
                    new ExpectedElementException("Expected name after FUN token"),
                    parser.Cursor
                );
            }

            parser.TryEat(TokenType.L_PAREN, false);

            parameters = Utils.ParseSequence(parser, Name.Consume);

            parser.TryEat(TokenType.R_PAREN, false);

            block = parser.TryConsumer((Parser p) => Block.Consume(p));

            return new FuncDecl(name, parameters, block);
        }
    }
}
