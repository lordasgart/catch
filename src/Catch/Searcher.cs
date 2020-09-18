using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Catch
{
    public class Searcher
    {
        private readonly IFileSystemReader _fileSystemReader;

        public Searcher(IFileSystemReader fileSystemReader)
        {
            _fileSystemReader = fileSystemReader;
        }

        public IEnumerable<CatchInfo> Search(string path, string searchPattern, string content)
        {
            //var searchWords1 = content.Split(',');
            var searchWords2 = content.Split(' ');
            
            var searchWords = new List<string>();
            //searchWords.AddRange(searchWords1);
            searchWords.AddRange(searchWords2);
            
            var files = _fileSystemReader.GetFiles(path,searchPattern);
            foreach (var file in files)
            {
                var lines = _fileSystemReader.ReadLines(file);

                CatchInfo catchInfo = null;

                var lineNumber = 0;
                foreach (var line in lines)
                {
                    foreach (var searchWord in searchWords)
                    {
                        if (line.Contains(searchWord))
                        {
                            if (catchInfo == null)
                            {
                                catchInfo = new CatchInfo {FileInfo = new FileInfo(file)};
                                catchInfo.SearchWordHits = new Dictionary<string, int>();
                                foreach (var searchWordInit in searchWords)
                                {
                                    if (!catchInfo.SearchWordHits.ContainsKey(searchWordInit))
                                    {
                                        catchInfo.SearchWordHits.Add(searchWordInit, 0);
                                    }
                                }
                            }

                            catchInfo.SearchWordHits[searchWord]++;
                            catchInfo.LineHits.Add(new LineHit() { LineNumber = lineNumber, LineContent = line });
                        }
                    }

                    lineNumber++;
                }

                if (catchInfo != null)
                {
                    bool allSearchWordsHit = true;
                    foreach (var searchWordHit in catchInfo.SearchWordHits)
                    {
                        if (searchWordHit.Value == 0)
                        {
                            allSearchWordsHit = false;
                        }
                    }

                    if (allSearchWordsHit)
                    {
                        yield return catchInfo;
                    }
                }
            }
        }
    }

    public class CatchInfo
    {
        public FileInfo FileInfo { get; set; }
        public List<LineHit> LineHits { get; set; } = new List<LineHit>();
        public Dictionary<string, int> SearchWordHits { get; set; }
    }

    public class LineHit
    {
        public int LineNumber { get; set; }
        public string LineContent { get; set; }
    }
}
