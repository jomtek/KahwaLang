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

            Console.WriteLine("OK");

            Console.Write("Lexing...");

            var tokens = Lexing.Lexer.Go(code);

            Console.WriteLine($"OK\t{tokens.Length} tokens total");

            if (tokens.Length == 0)
                return;

            Console.Write("Parsing...");


        }
    }
}
