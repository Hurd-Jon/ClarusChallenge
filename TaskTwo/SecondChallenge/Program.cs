using ApacheParser.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecondChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get relative path to source file
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var filePath = Path.Combine(exePath, "..\\..\\..\\..\\..\\Files");
            var fileName = Path.Combine(filePath, "SecondChallenge.txt");
            
            // create path to output folder
            var destPath = Path.Combine(exePath, "..\\..\\..\\..\\Output");

            var rawLines = File.ReadAllLines(fileName);

            var lines = new List<LogLine>();

            // Load lines 
            foreach (var line in rawLines)
            {
                // ignore comments
                if (!(line.StartsWith("#")))
                {
                    Console.WriteLine(line);
                    var logLine = new LogLine(line);
                    lines.Add(logLine);
                }
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(destPath, "Results.txt")))
            {
                outputFile.WriteLine("IP,Field1,Field2,Date1,Date2");
                foreach (var line in lines)
                    outputFile.WriteLine(line.OutputFormat());
            }

        }

    }
}
