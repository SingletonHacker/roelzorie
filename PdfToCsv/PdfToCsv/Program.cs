namespace PdfToCsv
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pdfParser = new PdfParsercs();

            pdfParser.Parse();
        }
    }
}
