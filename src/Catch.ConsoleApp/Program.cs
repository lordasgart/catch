using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Shell.NET;

namespace Catch.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];
            DirectoryInfo di = new DirectoryInfo(path);
            Console.WriteLine($"Path: {di.FullName}");
            var searchPattern = args[1];
            Console.WriteLine($"SearchPattern: {searchPattern}");
            var searchWords = new List<string>();
            if (args.Length > 2)
            {
                for (var index = 2; index < args.Length; index++)
                {
                    var arg = args[index];
                    searchWords.Add(arg);
                }
            }
            
            var content = string.Join(' ', searchWords);
            Console.WriteLine($"Content: {content}");
            
            IFileSystem fileSystem = new FileSystem();
            var fileSystemReader = new SimpleFileSystemReader(fileSystem);
            var searcher = new Searcher(fileSystemReader);
            var results = searcher.Search(path, searchPattern , content);

            foreach (var result in results)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"file://{result.FileInfo.FullName.Replace(" ","%20")}");
                Console.ResetColor();
                Console.WriteLine($"{result.FileInfo.FullName}");
                
                foreach (var lineHit in result.LineHits)
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"  {lineHit.LineNumber}: ");
                    Console.ResetColor();
                    Console.WriteLine($"{lineHit.LineContent.Trim()}");
                }
            }
        }
        
        static void Main2(string[] args)
        {
            Console.WriteLine();

            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            foreach (var fore in colors)
            {
                foreach (var back in colors)
                {
                    Console.ForegroundColor = fore;
                    Console.BackgroundColor = back;
                    Console.Write(" *** ");

                    Console.ResetColor();
                    Console.Write("  ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
