using Kahwa.Lexing;
using Kahwa.Parsing.Exceptions;
using System;
using System.Linq;

namespace Kahwa.Parsing
{
    public class Parser
    {
        private readonly Token[] _tokens;
        private int _lookAheadIndex = 0;
        public CodePosition Cursor { get; private set; }

        private int _tokensEaten = 0;
        public int TokensEaten
        {
            get { return _tokensEaten; }
        }

        public Parser(Token[] tokens)
        {
            _tokens = tokens;
            Cursor = new CodePosition(0, 0);
        }

        public Token LookAhead()
        {
            if (_lookAheadIndex >= _tokens.Length)
                throw new ParserException(new EOF(), Cursor);
            return _tokens[_lookAheadIndex];
        }

        public bool AreTokensRemaining()
        {
            return _lookAheadIndex < _tokens.Length;
        }

        public Token TryEat(TokenType tokenType, bool optional = true)
        {
            if (LookAhead().Type != tokenType)
            {
                if (optional)
                    throw new ParserException(new EatTokenFailed(tokenType), Cursor);
                else
                    throw new ParserException(new ExpectedTokenException(tokenType), Cursor);
            }

            if (LookAhead().Type != TokenType.EOL)
                Cursor = LookAhead().Pos;
            else
                Cursor = new CodePosition(LookAhead().Pos.Line + 1, 1);

            _tokensEaten++; // Statistics

            _lookAheadIndex++;
            return _tokens[_lookAheadIndex - 1];
        }

        public Token TryManyEats(TokenType[] tokenTypes, bool optional = true)
        {
            ParserException lastError = null;

            foreach (TokenType tokType in tokenTypes)
            {
                try
                {
                    return TryEat(tokType, optional);
                }
                catch (ParserException ex)
                {
                    lastError = ex;
                }
            }

            throw lastError;
        }

        public T TryConsumer<T>(Func<Parser, T> consumer)
        {
            int oldIndex = _lookAheadIndex;

            try
            {
                return consumer(this);
            }
            catch (ParserException)
            {
                _lookAheadIndex = oldIndex;
                throw;
            }
        }

        public T TryManyConsumers<T>(Func<Parser, T>[] consumers)
        {
            ParserException lastError = null;

            foreach (var consumer in consumers)
            {
                try
                {
                    return TryConsumer(consumer);
                }
                catch (ParserException ex)
                {
                    if (ex.IsExceptionFictive())
                    {
                        lastError = ex;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            throw lastError;
        }

        public void SkipThroughEOLs()
        {
            while (true)
            {
                try
                {
                    TryEat(TokenType.EOL);
                }
                catch (ParserException)
                {
                    break;
                }
            }
        }
    }
}
