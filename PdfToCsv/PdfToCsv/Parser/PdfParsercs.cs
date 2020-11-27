using System;
using System.Collections.Generic;
using System.IO;

namespace PdfToCsv.Parser
{
    public class PdfParsercs
    {
        private readonly List<string> _file = new List<string>();

        public List<OutputLine> Parse(string path)
        {
            var output = new List<OutputLine>();

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

            var startSearchArea = FindStartIndex();
            var endSearchArea = FindEndOfSections(startSearchArea);

            var sections = FindSections(startSearchArea, endSearchArea);

            var sectionParser = new SectionParser();

            var docId = GetDocumentNumber();

            foreach (var section in sections)
            {
                var outLine = sectionParser.Parse(section);

                outLine.DocumentNumber = docId;
                output.Add(outLine);
            }

            return output;
        }

        private int FindEndOfSections(int searchFromIndex)
        {
            for (int i = searchFromIndex; i < _file.Count; i++)
            {
                if (_file[i].StartsWith("Totaal nettowaarde"))
                {
                    return i - 1;
                }
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
                if (_file[i].StartsWith(alternativeEnd))
                {
                    for (int iReverse = i; iReverse > 0; iReverse--)
                    {
                        // Reverse search because somethimes the "end of section" is preceded by
                        // adress information and somethimes not
                        if (_file[iReverse].StartsWith(" "))
                        {
                            return iReverse;
                        }
                        if (iReverse == 1)
                        {
                            throw new ArgumentException("Reverse search failed");
                        }
                    }
                }
                if (_file[i].StartsWith(nextOrderNumber))
                {
                    return i - 1;
                }
            }
            throw new ArgumentException("Could not find end of section");
        }

        private List<Section> FindSections(int startOfSearchArea, int endOfSearchArea)
        {
            var sections = new List<Section>();

            var currentLineNumber = 1;

            var startOfSection = FindStartOfSection(startOfSearchArea, currentLineNumber);
            var endOfLastSection = FindEndOfSection(startOfSection, currentLineNumber);
            sections.Add(CreateSection(startOfSection, endOfLastSection));

            while (endOfLastSection + 1 != endOfSearchArea)
            {
                startOfSection = FindStartOfSection(endOfLastSection, ++currentLineNumber);
                endOfLastSection = FindEndOfSection(startOfSection, currentLineNumber);
                sections.Add(CreateSection(startOfSection, endOfLastSection));
            }

            return sections;
        }

        private Section CreateSection(int start, int end)
        {
            return new Section
            {
                StartIndex = start,
                EndIndex = end,
                Data = _file.GetRange(start, end - start + 1)
            };
        }

        private string GetDocumentNumber()
        {
            var index = -1;
            for (int i = 0; i < _file.Count; i++)
            {
                if (_file[i].StartsWith("Documentnummer"))
                {
                    index = i + 1;
                }
            }

            if (index == -1)
            {
                throw new ArgumentException("Could not find start of section");
            }

            var line = _file[index].Trim();

            return line.Substring(0, 10);
        }
    }
}
