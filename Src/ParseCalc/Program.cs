namespace ParseCalc;

static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            PrintHelp();
            return;
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

            Or if you want to use the Windows Forms interface just pass the ui argument like so. 
            ParseCalc -ui
            """);
    }

    static void OneArgs(string arg)
    {
        if (arg.EqualsIgnoreCase("ui") || arg.EqualsIgnoreCase("-ui"))
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            return;
        }

        string[] lines = arg.Replace("\\n", "\n").SplitIntoLines();
        decimal[] results = Math.Parse(lines).Select(i => i ?? 0).ToArray();

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

        int longestLine = lines.Select(l => l.Length).Max();
        int longestResult = results.Select(i => i.ToString("n0").Length).Max();

        for (int i = 0; i < results.Length; i++)
        {
            Console.WriteLine($"{lines[i].PadRight(longestLine)} : {results[i].ToString("n0").PadLeft(longestResult)}{results[i].ToDecimals()}");
        }
    }
}
