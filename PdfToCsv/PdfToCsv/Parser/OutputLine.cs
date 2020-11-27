namespace PdfToCsv.Parser
{
    public class OutputLine
    {
        public string DocumentNumber { get; set; }

        public string ArticleName { get; set; }

        public string CustomerOrderNumber { get; set; }

        public string LineNumber { get; set; }

        public string Amount { get; set; }

        public string PricePerUnit { get; set; }

        public string TotalPrice { get; set; }
    }
}
