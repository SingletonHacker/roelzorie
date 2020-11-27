using System;
using System.Collections.Generic;
using System.IO;

namespace PdfToCsv
{
    public class CsvWriter
    {
        private readonly List<string> _csvFile = new List<string>();

        public void AddHeader(IEnumerable<string> columns)
        {
            var header = string.Join(",", columns);

            if (_csvFile.Count > 0)
            {
                throw new ArgumentException("File already populated, cannot add header");
            }

            _csvFile.Add(header);
        }

        public void AddLine(IEnumerable<string> columns)
        {
            var line = string.Join(",", columns);

            _csvFile.Add(line);
        }

        public void CreateFile(string pathWithoutExtention)
        {
            File.WriteAllLines(pathWithoutExtention + ".csv", _csvFile);
        }
    }
}
