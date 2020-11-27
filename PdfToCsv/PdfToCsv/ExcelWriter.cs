using System.Collections.Generic;
using System.IO;
using PdfToCsv.Parser;

namespace PdfToCsv
{
    public class ExcelWriter
    {
        private const string delimeter = ";";

        public void Write(string outputPath, List<OutputLine> parsedLines)
        {
            var lines = new List<string>();
            lines.Add($"sep={delimeter}");

            var header = @$"{nameof(OutputLine.DocumentNumber)}{delimeter}{nameof(OutputLine.ArticleName)}{delimeter}{nameof(OutputLine.CustomerOrderNumber)}{delimeter}{nameof(OutputLine.LineNumber)}{delimeter}{nameof(OutputLine.Amount)}{delimeter}{nameof(OutputLine.PricePerUnit)}{delimeter}{nameof(OutputLine.TotalPrice)}";

            lines.Add(header);

            foreach (var line in parsedLines)
            {
                var lineToAdd = $"{line.DocumentNumber}{delimeter}{line.ArticleName}{delimeter}{line.CustomerOrderNumber}{delimeter}{line.LineNumber}{delimeter}{line.Amount}{delimeter}{line.PricePerUnit}{delimeter}{line.TotalPrice}";
                lines.Add(lineToAdd);
            }

            File.WriteAllLines(outputPath, lines);
        }
    }
}
