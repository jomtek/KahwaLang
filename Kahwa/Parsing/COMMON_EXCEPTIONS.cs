using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing
{
    // Below is a list of all the possible parser exceptions
    // Most code below has been copied from another repository of mine https://github.com/Jomtek/LazenLang

    // Fictive exceptions
    public struct EatTokenFailed : IParserExceptionContent
    {
        public readonly TokenType TokenType;
        public EatTokenFailed(TokenType tokenType)
        {
            TokenType = tokenType;
        }
    }

    public struct ConsumerFailed : IParserExceptionContent { }
    public struct EOF : IParserExceptionContent { }


    // Dramatic exceptions
    public struct UnexpectedTokenException : IParserExceptionContent
    {
        public readonly TokenType TokenType;
        public UnexpectedTokenException(TokenType tokenType)
        {
            TokenType = tokenType;
        }
    }

    public struct ExpectedTokenException : IParserExceptionContent
    {
        public readonly TokenType TokenType;
        public ExpectedTokenException(TokenType tokenType)
        {
            TokenType = tokenType;
        }
    }

    public struct UnexpectedElementException : IParserExceptionContent
    {
        public readonly string Message;
        public UnexpectedElementException(string message)
        {
            Message = message;
        }
    }

    public struct ExpectedElementException : IParserExceptionContent
    {
        public readonly string Message;
        public ExpectedElementException(string message)
        {
            Message = message;
        }
    }

    public struct InvalidCharLit : IParserExceptionContent
    {
        public readonly string Value;
        public InvalidCharLit(string value)
        {
            Value = value;
        }
    }
}
