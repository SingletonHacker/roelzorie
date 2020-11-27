using System.Collections.Generic;

namespace PdfToCsv
{
    public class Section
    {
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public List<string> Data { get; set; }
    }
}
