using System;
using System.IO.Abstractions;

namespace Catch.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileSystem fileSystem = new FileSystem();
            var fileSystemReader = new SimpleFileSystemReader(fileSystem);
            var searcher = new Searcher(fileSystemReader);
            var results = searcher.Search("/home/lordasgart/Nextcloud/Notes", "*.md", "git credentials");

            foreach (var result in results)
            {
                Console.WriteLine($"file://{result.FileInfo.FullName.Replace(" ","%20")}");
                Console.WriteLine($"{result.FileInfo.FullName}");
                foreach (var lineHit in result.LineHits)
                {
                    Console.WriteLine($"  {lineHit.LineNumber}: {lineHit.LineContent}");
                }
            }
        }
    }
}
