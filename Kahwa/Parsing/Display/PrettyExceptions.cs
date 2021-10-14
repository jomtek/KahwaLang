using Kahwa.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Display
{
    public static class PrettyExceptions
    {
        public static string PrettyEx(ParserException ex)
        {
            return $"error: {ex.Position.Line}:{ex.Position.Column}: {ex.Content.GetType().Name}: {PrettyContent(ex.Content)}";
        }

        public static string PrettyContent(IParserExceptionContent content)
        {
            switch (content)
            {
                case UnexpectedTokenException x:
                    return x.TokenType.ToString();
                case ExpectedTokenException x:
                    return x.TokenType.ToString();
                case UnexpectedElementException x:
                    return x.Message;
                case ExpectedElementException x:
                    return x.Message;
                case InvalidCharLit x:
                    return $"'{x.Value}'";
                default:
                    break;
            }

            throw new ArgumentException();
        }
    }
}
