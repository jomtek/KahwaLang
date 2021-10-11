using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kahwa.Lexing
{
    public enum TokenType
    {
        L_PAREN, R_PAREN, L_BRACKET, R_BRACKET, L_CURLY_BRACKET, R_CURLY_BRACKET, // ( )    [     ]      {     }
        COMMA, DOT, APOSTROPHE, QUOTE,                                            // ,      .     '      " 
        EOL,                                                                      // \n
        IDENTIFIER, INTEGER_LIT, DOUBLE_LIT, STRING_LIT, CHAR_LIT, BOOLEAN_LIT,   // baz    12    2.36   "yep" 'a'  true
        IN, FOR, RETURN, IF, ELSE, ELIF, WHILE, BREAK, CONTINUE, FUN,             // in     for   return if    else elif  while break continue fun
        LIST_TYPE, FUN_TYPE, NULL, LONG_TYPE, INT_TYPE, SHORT_TYPE, BYTE_TYPE,    // List   Fun   Null   Long  Int  Short Byte Double Float String Char Bool
        DOUBLE_TYPE, FLOAT_TYPE, STRING_TYPE, CHAR_TYPE, BOOL_TYPE,               // Double Float String Char  Bool
        ASSIGN, EQ, NOT_EQ, BOOLEAN_AND, BOOLEAN_OR, GREATER, LESS, PLUS, MINUS,  // =      ==    !=     &&    ||   >     <    +      -
        DIVIDE, MULTIPLY, POWER, MODULO, NOT, GREATER_EQ, LESS_EQ, PLUS_EQ,       // /      *     ^      %     !    >=    <=   +=
        MINUS_EQ, DIVIDE_EQ, MULTIPLY_EQ, POWER_EQ, MODULO_EQ,                    // -=     /=    *=     ^=    %=  
        SINGLE_LINE_COMMENT, MULTI_LINE_COMMENT, SPACE, TAB, UNKNOWN              // //     /**/  \s     \t    ???   
    }
}
