using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kahwa.Lexing
{
    public static class Lexer
    {
        private static string GetEscapeSequence(char c)
        {
            return "\\u" + ((int)c).ToString("X4");
        }

        public static Token[] Go(string code)
        {
            var result = new List<Token>();
            int colTrack = 1;
            int lineTrack = 1;

            while (code.Length > 0)
            {
                if (GetEscapeSequence(code[0]) == @"\u000D") // CR escape sequence, right before the LF (newline)
                {
                    code = code.Substring(1);
                    continue;
                }

                var table = Char.IsLetter(code[0]) ? TokenRegexes.IdenRegexTable : TokenRegexes.OtherRegexTable;
                foreach ((string, TokenType) regexPair in table)
                {
                    TokenType tokenType = regexPair.Item2;
                    Match match = new Regex(regexPair.Item1).Match(code);

                    if (match.Success)
                    {
                        int matchLength = match.Length;
                        string matchValue = match.Value;

                        if (tokenType != TokenType.SPACE && tokenType != TokenType.TAB)
                        {
                            if (tokenType == TokenType.STRING_LIT || tokenType == TokenType.CHAR_LIT)
                                matchValue = matchValue.Substring(1).Remove(matchLength - 2);

                            if (tokenType != TokenType.SINGLE_LINE_COMMENT && tokenType != TokenType.MULTI_LINE_COMMENT)
                                result.Add(new Token(matchValue.Trim(), tokenType, new CodePosition(lineTrack, colTrack)));
                        }

                        if (tokenType == TokenType.EOL)
                        {
                            colTrack = 1;
                            lineTrack++;
                        }
                        else
                        {
                            colTrack += match.Length;
                        }

                        code = code.Substring(match.Length);
                        break;
                    }
                }
            }

            return result.ToArray();
        }
    }
}
