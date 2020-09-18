using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace Catch
{
    public class SimpleFileSystemReader : IFileSystemReader
    {
        private readonly IFileSystem _fileSystem;

        private readonly List<string> _files = new List<string>();

        public SimpleFileSystemReader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            var files = _fileSystem.Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            return files;
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return _fileSystem.File.ReadLines(path, Encoding.UTF8);
        }
    }
}
