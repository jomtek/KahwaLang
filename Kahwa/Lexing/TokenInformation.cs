namespace Kahwa.Lexing
{
    public static class TokenInformation
    {
        public static TokenType[] AlgebraOperators =
        {
            TokenType.EQ,
            TokenType.NOT_EQ,
            TokenType.BOOLEAN_AND,
            TokenType.BOOLEAN_OR,
            TokenType.IN,
            TokenType.GREATER,
            TokenType.LESS,
            TokenType.PLUS,
            TokenType.MINUS,
            TokenType.DIVIDE,
            TokenType.MULTIPLY,
            TokenType.POWER,
            TokenType.DOT,
            TokenType.MODULO,
            TokenType.GREATER_EQ,
            TokenType.LESS_EQ,
            TokenType.CAST,
        };

        public static TokenType[] MutationOperators =
        {
            TokenType.ASSIGN,
            TokenType.MINUS_EQ,
            TokenType.PLUS_EQ,
            TokenType.MULTIPLY_EQ,
            TokenType.DIVIDE_EQ,
            TokenType.POWER_EQ,
            TokenType.MODULO_EQ,
        };

        public static TokenType[] Literals =
        {
            TokenType.IDENTIFIER,
            TokenType.BOOLEAN_LIT,
            TokenType.CHAR_LIT,
            TokenType.DOUBLE_LIT,
            TokenType.INTEGER_LIT,
            TokenType.STRING_LIT,
        };

        public static TokenType[] PrimitiveTypes =
        {
            TokenType.LIST_TYPE,
            TokenType.FUN_TYPE,
            TokenType.LONG_TYPE,
            TokenType.INT_TYPE,
            TokenType.SHORT_TYPE,
            TokenType.BYTE_TYPE,
            TokenType.DOUBLE_TYPE,
            TokenType.FLOAT_TYPE,
            TokenType.STRING_TYPE,
            TokenType.CHAR_TYPE,
            TokenType.BOOL_TYPE,
        };

        public static (string, TokenType)[] IdenRegexTable = new (string, TokenType)[]
        {
            (@"^(true|false)",                        TokenType.BOOLEAN_LIT),
            (@"^fun(?![a-zA-Z_0-9])",                 TokenType.FUN),
            (@"^for(?![a-zA-Z_0-9])",                 TokenType.FOR),
            (@"^return(?![a-zA-Z_0-9])",              TokenType.RETURN),
            (@"^if(?![a-zA-Z_0-9])",                  TokenType.IF),
            (@"^else(?![a-zA-Z_0-9])",                TokenType.ELSE),
            (@"^elif(?![a-zA-Z_0-9])",                TokenType.ELIF),
            (@"^while(?![a-zA-Z_0-9])",               TokenType.WHILE),
            (@"^break(?![a-zA-Z_0-9])",               TokenType.BREAK),
            (@"^continue(?![a-zA-Z_0-9])",            TokenType.CONTINUE),
            (@"^in(?![a-zA-Z_0-9])",                  TokenType.IN),
            
            (@"^List(?![a-zA-Z_0-9])",                TokenType.LIST_TYPE),
            (@"^Fun(?![a-zA-Z_0-9])",                 TokenType.FUN_TYPE),
            (@"^Long(?![a-zA-Z_0-9])",                TokenType.LONG_TYPE),
            (@"^Int(?![a-zA-Z_0-9])",                 TokenType.INT_TYPE),
            (@"^Short(?![a-zA-Z_0-9])",               TokenType.SHORT_TYPE),
            (@"^Byte(?![a-zA-Z_0-9])",                TokenType.BYTE_TYPE),
            (@"^Double(?![a-zA-Z_0-9])",              TokenType.DOUBLE_TYPE),
            (@"^Float(?![a-zA-Z_0-9])",               TokenType.FLOAT_TYPE),
            (@"^String(?![a-zA-Z_0-9])",              TokenType.STRING_TYPE),
            (@"^Char(?![a-zA-Z_0-9])",                TokenType.CHAR_TYPE),
            (@"^Bool(?![a-zA-Z_0-9])",                TokenType.BOOL_TYPE),

            (@"^Null(?![a-zA-Z_0-9])",                TokenType.NULL),
            
            (@"^(?![0-9])[0-9_a-zA-Z\u00C0-\u017F]+", TokenType.IDENTIFIER),
        };

        public static (string, TokenType)[] OtherRegexTable = new (string, TokenType)[]
        {
            (@"^\t",                                  TokenType.TAB),
            (@"^\n",                                  TokenType.EOL),
            (@"^\s",                                  TokenType.SPACE),
            (@"^\(",                                  TokenType.L_PAREN),
            (@"^\)",                                  TokenType.R_PAREN),
            (@"^\,",                                  TokenType.COMMA),
            (@"^\.",                                  TokenType.DOT),
            (@"^[0-9]+",                              TokenType.INTEGER_LIT),
            (@"^\""[^\""\\]*(\\.[^\""\\]*)*\""",      TokenType.STRING_LIT),
            (@"^\'[^\'\\]*(\\.[^\'\\]*)*\'",          TokenType.CHAR_LIT),
            (@"^[0-9]+\.[0-9]+",                      TokenType.DOUBLE_LIT),
            (@"^\[",                                  TokenType.L_BRACKET),
            (@"^\]",                                  TokenType.R_BRACKET),
            (@"^\{",                                  TokenType.L_CURLY_BRACKET),
            (@"^\}",                                  TokenType.R_CURLY_BRACKET),
            (@"^\/\/(.*)",                            TokenType.SINGLE_LINE_COMMENT),
            (@"^\/\*((((?!\/\*)(?!\*\/).)|\n)*)\*\/", TokenType.MULTI_LINE_COMMENT),
            (@"^\%",                                  TokenType.MODULO),
            (@"^\^",                                  TokenType.POWER),
            (@"^\+\=",                                TokenType.PLUS_EQ),
            (@"^\-\=",                                TokenType.MINUS_EQ),
            (@"^\/\=",                                TokenType.DIVIDE_EQ),
            (@"^\*\=",                                TokenType.MULTIPLY_EQ),
            (@"^\^\=",                                TokenType.POWER_EQ),
            (@"^\%\=",                                TokenType.MODULO_EQ),
            (@"^\+",                                  TokenType.PLUS),
            (@"^\-",                                  TokenType.MINUS),
            (@"^\|\|",                                TokenType.BOOLEAN_OR),
            (@"^\&\&",                                TokenType.BOOLEAN_AND),
            (@"^\>\=",                                TokenType.GREATER_EQ),
            (@"^\<\=",                                TokenType.LESS_EQ),
            (@"^\=\=",                                TokenType.EQ),
            (@"^\!\=",                                TokenType.NOT_EQ),
            (@"^\>",                                  TokenType.GREATER),
            (@"^\<",                                  TokenType.LESS),
            (@"^\/",                                  TokenType.DIVIDE),
            (@"^\*",                                  TokenType.MULTIPLY),
            (@"^\=",                                  TokenType.ASSIGN),
            (@"^\!",                                  TokenType.NOT),
            (@"^\'",                                  TokenType.APOSTROPHE),
            (@"^\""",                                 TokenType.QUOTE),
            (@"^.",                                   TokenType.UNKNOWN),
        };
    }
}
