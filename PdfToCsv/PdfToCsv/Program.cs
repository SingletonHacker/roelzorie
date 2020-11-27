using System;
using System.IO;
using PdfToCsv.Parser;

namespace PdfToCsv
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var inPath = @"C:\git\Personal\assets\factuur.txt";
            //var outPath = @"C:\git\Personal\assets\factuur.csv";

            if (args.Length != 3)
            {
                LogInfo("Could not find enough command line arguments");
                LogInfo("Usage of tool: PdfToCsv.Exe <indir: path> <outdir: path> <removeParsedFiles : true/false");
                LogInfo(@"Example: PdfToCsv.Exe C:\temp\in C:\temp\out false");
                return;
            }

            var inDir = args[0];
            var outDir = args[1];
            bool.TryParse(args[2], out var removeParsedFile);

            LogInfo($"Reading from directory {inDir}");

            var files = Directory.GetFiles(inDir);

            LogInfo($"Found {files.Length} files");
            LogInfo($"-----------------------------");

            foreach (var file in files)
            {
                var outPath = GetOutPath(file, outDir);

                var pdfParser = new PdfParsercs();
                var writer = new ExcelWriter();

                LogInfo($"Parsing {file}");
                var lines = pdfParser.Parse(file);

                LogInfo($"Writing {outPath}");
                writer.Write(outPath, lines);

                if (removeParsedFile)
                {
                    LogInfo($"Removing {file}");
                    File.Delete(file);
                }

                LogInfo($"-----------------------------");
            }
        }

        private static string GetOutPath(string inPath, string outDir)
        {
            var fileName = Path.GetFileNameWithoutExtension(inPath);

            return $"{Path.Combine(outDir, fileName)}.csv";
        }

        private static void LogInfo(string line)
        {
            var defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);

            Console.ForegroundColor = defaultColor;
        }
    }
}
