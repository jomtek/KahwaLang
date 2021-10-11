using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;

namespace Kahwa.Parsing
{
    // This class represents an identifier.
    // In Kahwa, any identifier, in any situation, can be explicitly type-restricted.
    // Example: func foo: String() {}  This method must return a String object
    // Example: baz: Int = 5
    //          baz = 'c' // Compile-time error
    // Example: func foo(a: String, b: Int)
    // Example: foo(myComplexVariable: String)
    public class Name
    {
        public readonly Token Identifier;
        public readonly TypeNode Type;

        public Name(Token identifier, TypeNode type = null)
        {
            Identifier = identifier;
            Type = type;
        }

        public static Name Consume(Parser parser)
        {
            Token identifier;
            TypeNode type = null;

            identifier = parser.TryEat(TokenType.IDENTIFIER);

            bool colon = true;
            try
            {
                parser.TryEat(TokenType.COLON);
            }
            catch (ParserException)
            {
                colon = false;
            }

            if (colon)
            {
                try
                {
                    type = parser.TryConsumer(TypeNode.Consume);
                }
                catch (ParserException ex)
                {
                    if (!ex.IsExceptionFictive()) throw;
                    throw new ParserException(
                        new ExpectedElementException("Expected type after COLON token"),
                        parser.Cursor
                    );
                }
            }

            return new Name(identifier, type);
        }
    }
}
