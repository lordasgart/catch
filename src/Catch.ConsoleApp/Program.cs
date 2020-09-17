using System;

namespace Catch.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello I am \"Catch\"!");
            Console.WriteLine();
            Console.WriteLine("I will find files for you on the command line");
            Console.WriteLine("and output the results like Agent Ransack");

            Searcher searcher = new Searcher();
        }
    }
}
