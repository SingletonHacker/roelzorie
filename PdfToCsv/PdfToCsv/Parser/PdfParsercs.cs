using System;
using System.Collections.Generic;
using System.IO;

namespace PdfToCsv
{
    public class PdfParsercs
    {
        private readonly List<string> _file = new List<string>();

        public void Handle()
        {
            var path = "";

            using (var sr = new StreamReader(path))
            {
                var line = sr.ReadLine();

                while (line != null)
                {
                    _file.Add(line);
                    line = sr.ReadLine();
                }
            }

            if (_file == null || _file.Count == 0)
            {
                throw new ArgumentException("could not parse file");
            }

            var index = FindStartIndex();

            var firstLineNumber = 1;

            var firstSection = FindStartOfSection(index, firstLineNumber);
            var endOfSections = FindEndOfSections(firstSection);
        }

        private int FindEndOfSections(int searchFromIndex)
        {
            var index = 0;
            for (int i = searchFromIndex; i < _file.Count; i++)
            {
                if (_file[i].StartsWith("Totaal nettowaarde:"))
                {
                    return index;
                }
                index++;
            }

            throw new ArgumentException("Could not find end of sections");
        }

        private int FindStartIndex()
        {
            var index = 0;
            foreach (var line in _file)
            {
                if (line.StartsWith("Afrekeningspositie vestiging:"))
                {
                    return index;
                }
                index++;
            }

            throw new ArgumentException("Could not find starting area");
        }

        private int FindStartOfSection(int searchFromIndex, int pdfLineNumber)
        {
            var seachFor = string.Format("{0:D5}", pdfLineNumber);
            for (int i = searchFromIndex; i < _file.Count; i++)
            {
                if (_file[i].StartsWith(seachFor))
                {
                    return i;
                }
            }
            throw new ArgumentException("Could not find start of section");
        }

        private int FindEndOfSection(int searchFromIndex, int lastPdfLineNumber)
        {
            var nextOrderNumber = string.Format("{0:D5}", lastPdfLineNumber + 1);
            var alternativeEnd = "____________________";

            for (int i = searchFromIndex; i < _file.Count; i++)
            {
                if (_file[i].StartsWith(nextOrderNumber) || _file[i].StartsWith(alternativeEnd))
                {
                    return i;
                }
            }
            throw new ArgumentException("Could not find end of section");
        }
    }
}
