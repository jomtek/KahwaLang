using Kahwa.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Parsing.Exceptions
{
    public class ParserException : Exception
    {
        public readonly IParserExceptionContent Content;
        public readonly CodePosition Position;

        public ParserException(IParserExceptionContent content, CodePosition position)
        {
            Content = content;
            Position = position;
        }

        public bool IsExceptionFictive()
        {
            return Content is EatTokenFailed ||
                   Content is ConsumerFailed ||
                   Content is EOF;
        }
    }
}
