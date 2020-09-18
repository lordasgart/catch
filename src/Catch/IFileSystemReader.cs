using System.Collections.Generic;

namespace Catch
{
    public interface IFileSystemReader
    {
        IEnumerable<string> GetFiles(string pattern, string path);
        IEnumerable<string> ReadLines(string path);
    }
}
