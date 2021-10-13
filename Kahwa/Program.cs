using Kahwa.Parsing;
using Kahwa.Parsing.Exceptions;
using Kahwa.Parsing.AST.Expressions;
using System;
using System.IO;

namespace Kahwa
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Reading...");
            
            string code = File.ReadAllText("code.khw");

            Console.WriteLine("\tOK");

            Console.Write("Lexing...");

            var tokens = Lexing.Lexer.Go(code);

            Console.WriteLine($"\t{tokens.Length} tokens total");

            if (tokens.Length == 0)
                return;

            Console.Write("Parsing...");

            var parser = new Parser(tokens);
            
            Block ast;
            try
            {
                ast = Block.Consume(parser, false);
            }
            catch (ParserException ex)
            {
                if (ex.Content is not EOF)
                {
                    Console.WriteLine("\n|");
                    Console.WriteLine("'-->  " + Parsing.Display.PrettyExceptions.PrettyEx(ex));
                    return;
                }
            }

            Console.WriteLine($"\t{parser.TokensEaten} tokens eaten !");


        }
    }
}
