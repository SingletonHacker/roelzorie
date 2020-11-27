using System.Linq;

namespace PdfToCsv.Parser
{
    public class SectionParser
    {
        public OutputLine Parse(Section section)
        {
            var articleName = GetArticleName(section);
            var numbers = GetOrderAndLineNumberCustomer(section);
            var others = ReadAmountPricePerPAndTotalPrice(section);

            return new OutputLine
            {
                ArticleName = articleName,
                CustomerOrderNumber = numbers.Item1,
                LineNumber = numbers.Item2,
                Amount = others.Item1,
                PricePerUnit = others.Item2,
                TotalPrice = others.Item3
            };
        }

        private string GetArticleName(Section section)
        {
            var amountOfInfoLines = 3;
            var defaultAmountHeaderLines = 1;
            var additionalHeaderLines = section.Data.Count - amountOfInfoLines - defaultAmountHeaderLines;

            var firstLine = section.Data.First();

            var articleName = firstLine.Substring(5).Trim();

            if (additionalHeaderLines > 0)
            {
                for (int i = 1; i < additionalHeaderLines + 1; i++)
                {
                    articleName += $" {section.Data[i].Trim()}";
                }
            }

            return articleName;
        }

        private (string, string) GetOrderAndLineNumberCustomer(Section section)
        {
            var lineIndex = section.Data.Count - 1 - 1;

            var line = section.Data[lineIndex].Trim();
            var firstSlash = line.IndexOf(@"/");

            var orderNumber = line.Substring(0, firstSlash);

            var lineNumber = line.Substring(firstSlash + 1 + 1, 5);

            return (orderNumber, lineNumber);
        }

        private (string, string, string) ReadAmountPricePerPAndTotalPrice(Section section)
        {
            var line = section.Data.Last().Trim();

            var splittedLine = line.Split(" ");

            var amount = splittedLine[0];
            var pricePerP = splittedLine[2];
            var totalPrice = splittedLine[3];

            return (amount, pricePerP, totalPrice);
        }
    }
}
