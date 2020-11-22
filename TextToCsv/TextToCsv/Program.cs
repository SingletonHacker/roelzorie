using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextToCsv
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var directory = @"C:\git\Personal\roelzorie\TextToCsv\TextToCsv\bin\Debug\netcoreapp3.1\";
            var fileName = Path.Combine(directory, "tekst.txt");

            var txtFilePath = Path.Combine(directory, fileName);

            var file = ReadFile(txtFilePath);
            var newFile = Transform(file);

            File.WriteAllLines(Path.Combine(directory, Path.GetFileNameWithoutExtension(txtFilePath) + ".csv"), newFile);
        }

        private static List<string> ReadFile(string path)
        {
            var lines = new List<string>();
            using (var streamReader = new StreamReader(path))
            {
                var line = streamReader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = streamReader.ReadLine();
                }
            }
            return lines;
        }

        private static List<string> Transform(List<string> contents)
        {
            var newFile = new List<string>();

            foreach (var line in contents)
            {
                var sb = new StringBuilder();
                var columns = line.Split(' ');

                foreach (var column in columns)
                {
                    sb.Append(column + ',');
                }

                newFile.Add(sb.ToString());
            }

            return newFile;
        }
    }
}
