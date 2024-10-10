using ParseCalc;
using System.Diagnostics;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintHelp();
            return;
        }

        if (args[0].Replace("-", "").Equals("open", StringComparison.OrdinalIgnoreCase))
        {
            string filename = args.Length == 0 
                ? args[0]
                : Path.GetTempFileName() + ".calc";
            
            OpenFile(filename);
        }

        OneArgs(args[0]);
    }

    static void PrintHelp()
    {
        string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "";
        Console.WriteLine(
            $"""

        ParseCalc {version}

        ParseCalc will parse a string containing simple math functions and print out the results. Just pass in a string like so.
        ParseCalc "2 * 8 * 27"

        You can also split out the string into lines with the \n escape character like so.
        ParseCalc "2 * 8 \n27 - 4"

        Or if you want to edit in your favourite editor pass the open argument
        ParseCalc -open
        """);
    }

    static void OneArgs(string arg)
    {
        string[] lines = arg.Replace("\\n", "\n").SplitIntoLines();
        decimal[] results = ParseCalc.Math.Parse(lines).Select(i => i ?? 0).ToArray();

        if (results.Length == 0 || lines.Length != results.Length)
        {
            return;
        }
        else if (results.Length == 1)
        {
            Console.WriteLine(results.First());
            return;
        }

        // Add total
        lines = [.. lines, "Total"];
        results = [.. results, results.Sum()];

        int longestLine = lines.Max(l => l.Length);
        int longestResult = results.Max(i => i.ToString("n0").Length);

        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"{lines[i].PadRight(longestLine)} : {results[i].ToString("n0").PadLeft(longestResult)}{results[i].ToDecimals()}");
        }
    }

    private static string _fileName = "";
    private static FileSystemWatcher _watcher;
    static void OpenFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            File.WriteAllText(fileName, "");
        }

        OpenWithDefaultProgram(fileName);

        _watcher = new FileSystemWatcher(Path.GetDirectoryName(fileName));
        _watcher.Changed += File_Changed;
        _watcher.Filter = "*.calc";
        _watcher.IncludeSubdirectories = true;
        _watcher.EnableRaisingEvents = true;
        _watcher.NotifyFilter =
                                   NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Size;


        Console.WriteLine("Press enter to exit.");
        Console.ReadLine();
    }

    private static void File_Changed(object sender, FileSystemEventArgs e)
    {
        _watcher.EnableRaisingEvents = false;
        Console.WriteLine("File changed: " + DateTime.Now.ToString());

        FileStream? fs = OpenWhenUnloaked(_fileName);
        if (fs == null)
        {
            Console.WriteLine("File locked by another process");
        }

        string[] lines = ReadFile(fs);
        if (lines.Length == 0)
        {
            fs.Close();
            _watcher.EnableRaisingEvents = true;
            return;
        }

        var result = lines
            .Where(i => !Regex.IsMatch(i, @"^\s*Total\s*:\s*[0-9\.,]*"))
            .Select(i => TidyLine(i))
            .Select(i => new KeyValuePair<string, decimal?>(i, ParseCalc.Math.ParseLine(i)))
            .ToArray();

        int longestLine = lines.Max(l => l.Length);
        int longestResult = result.Max(i => (i.Value ?? 0).ToString("n0").Length);
        decimal total = result.Sum(i => i.Value ?? 0);
        string[] output = result
            .Append(new KeyValuePair<string, decimal?>("Total", total))
            .Select(i => FormatLine(i.Key, i.Value, longestLine, longestResult))
            .ToArray();

        WriteToFile(fs, output);
        fs.Close();
        _watcher.EnableRaisingEvents = true;
    }

    static string FormatLine(string line, decimal? result, int longestLine, int longestResult) =>
        result == null
            ? line.TrimEnd()
            : $"{line.PadRight(50)} : {result.Value.ToString("n0").PadLeft(longestResult)}{result.Value.ToDecimals()}";

    static string TidyLine(string line) => 
        line.Contains(':')
            ? line[..line.IndexOf(':')].TrimEnd()
            : line.TrimEnd();

    static void OpenWithDefaultProgram(string fileName)
    {
        _fileName = fileName;
        using Process fileopener = new();

        fileopener.StartInfo.FileName = "explorer";
        fileopener.StartInfo.Arguments = "\"" + fileName + "\"";
        fileopener.Start();
    }

    static FileStream? OpenWhenUnloaked(string fileName)
    {
        bool fileIsLocked = true;
        int trys = 1000;

        while (fileIsLocked && trys > 0)
        {
            try
            {
                FileStream fs = new(fileName, FileMode.Open, FileAccess.ReadWrite);
                return fs;
            }
            catch (IOException)
            {
                Thread.Sleep(10);
                trys--;
            }
        }

        return null;
    }

    static string[] ReadFile(FileStream fs)
    {
        StreamReader sr = new(fs);
        List<string> lines = [];

        string? line = sr.ReadLine();
        while (line != null)
        {
            lines.Add(line);
            line = sr.ReadLine();
        }

        return lines.ToArray();
    }

    static void WriteToFile(FileStream fs, string[] lines)
    {
        fs.Position = 0;
        StreamWriter sw = new(fs);
        foreach (string line in lines)
        {
            sw.WriteLine(line);
        }

        sw.Flush();
    }
}