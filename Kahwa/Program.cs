using Kahwa.Parsing;
using Kahwa.Parsing.Exceptions;
using Kahwa.Parsing.AST.Expressions;
using System;
using System.IO;
using Kahwa.Generation;

namespace Kahwa
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Reading...       ");
            
            string code = File.ReadAllText("code.khw");

            Console.WriteLine("\tOK");

            Console.Write("Lexing...         ");

            var tokens = Lexing.Lexer.Go(code);

            Console.WriteLine($"\t{tokens.Length} tokens total");

            if (tokens.Length == 0)
                return;

            Console.Write("Parsing...        ");

            var parser = new Parser(tokens);

            Block ast = null;
            try
            {
                ast = Block.Consume(parser, true);
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

            Console.Write($"Generating code...");

            
            BCGenerator generator;
            try
            {
                generator = new BCGenerator(ast);
            }
            catch (GenerationException ex)
            {
                Console.WriteLine("\n|");
                Console.WriteLine($"'--> error: {ex.Position.Line}:{ex.Position.Column}: GenerationException: {ex.Message}");
                return;
            }

            Console.WriteLine($"\tdone!");

            Console.WriteLine();
            Console.WriteLine();

            foreach (BCInstruction instruction in generator.Instructions)
            {
                var pretty = instruction.ToString(generator.SymbolTable, generator.ConstantTable);
                Console.WriteLine(pretty);
            }
            
        }
    }
}
