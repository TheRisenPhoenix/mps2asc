using System;
using System.IO;
using System.Linq;
using mps2asc;

class Program
{
    static void Main()
    {
        var args = Environment.GetCommandLineArgs();
        string path = "";
        if (args.Length >= 2)
        {
            path = args[1];
        }

        while (!File.Exists(path))
        {
            Console.WriteLine("Couldn't find the file. Please enter the path to the file (e.g. 'C:\\Users\\X\\Desktop\\file.mps')");
            path = Console.ReadLine() ?? string.Empty;
        }
        var outputFile = Path.ChangeExtension(path, "asc");

        var points = MpsParser.ParseXml(path);

        if (points.Count == 0)
        {
            Console.WriteLine("Apparently, something went wrong parsing...");
            Console.Read();
        }
        else
        {
            var stringifyPoints = points.Select(p => p.ToString());
            File.WriteAllLines(outputFile, stringifyPoints);

            Console.WriteLine($"{points.Count} points written to {outputFile}");
        }
    }
}