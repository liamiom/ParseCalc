namespace ParseCalc;

class Math
{
    public decimal?[] _results = [];

    private string _globalSymbol = "+";
    //public decimal? Total
    //{
    //    get
    //    {
    //        if (string.IsNullOrEmpty(_globalSymbol))
    //        {
    //            return null;
    //        }

    //        decimal? total = null;
    //        for (int i = 0; i < _results.Length; i++)
    //        {
    //            if (_results[i] != 0)
    //            {
    //                total = !total.HasValue 
    //                    ? _results[i] 
    //                    : DoMath(total.Value, _results[i].GetValueOrDefault(0), _globalSymbol);
    //            }
    //        }

    //        return total;
    //    }
    //}

    public static decimal?[] Parse(string text) => 
        Parse(text.SplitIntoLines());

    public static decimal?[] Parse(string[] lines)
    {
        lines = TrimOutSpaces(lines);
        lines = SetGlobalSymbol(lines);
        lines = TrimOutVariables(lines);
        return WorkoutLines(lines);
    }

    public static string[] TrimOutSpaces(string[] lines) => 
        lines.Select(i => i.Replace(" ", "")).ToArray();

    public static string[] SetGlobalSymbol(string[] lines)
    {
        if (lines.Length > 1 && lines[0].IsSymbol())
        {
            //_globalSymbol = lines[0];
            lines[0] = string.Empty;
        }

        return lines;
    }
    
    private static string[] TrimOutVariables(string[] lines)
    {
        Dictionary<string, decimal> vars = [];

        // Get the variables
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].RegexMatch("[a-z]=[0-9]"))
            {
                string[] split = lines[i].Split('=');
                vars.Add(split[0], decimal.Parse(split[1]));
                lines[i] = string.Empty;
            }
        }

        // Replace the variables wiht their value
        for (int i = 0; i < lines.Length; i++)
        {
            foreach (KeyValuePair<string, decimal> pair in vars)
            {
                lines[i] = lines[i].Replace(pair.Key, pair.Value.ToString());
            }
        }

        return lines;
    }

    private static decimal?[] WorkoutLines(string[] lines)
    {
        decimal?[] results = new decimal?[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(lines[i]))
            {
                decimal? result = ParseLine(lines[i]);
                if (result.HasValue)
                {
                    results[i] = result.Value;
                }
            }
        }

        return results;
    }

    private static decimal? ParseLine(string line)
    {
        // If there are brakets work the contents out first
        if (line.Contains('(') && line.Contains(')'))
        {
            int start = line.IndexOf('(');
            int end = line.LastIndexOf(')') + 1;
            int inSt = start + 1;
            int inEd = end - 1;
            line = string.Concat(
                line.AsSpan(0, start),
                ParseLine(line.Substring(inSt, inEd - inSt)).ToString(), 
                line.AsSpan(end));
        }

        decimal? total = null;
        string symbol = string.Empty;
        try
        {
            foreach (string item in line.SplitUp())
            {
                if (!total.HasValue)
                {
                    if (item.IsNumber())
                    {
                        total = decimal.Parse(item);
                    }
                }
                else
                {
                    if (item.IsNumber() && !string.IsNullOrEmpty(symbol))
                    {

                        decimal secondNumber = decimal.Parse(item);
                        total = DoMath(total.Value, secondNumber, symbol);
                    }
                    else
                    {
                        symbol = item;
                    }
                }
            }
        }
        catch
        {
            total = null;
        }

        return total;
    }

    public static decimal? DoMath(decimal firstNumber, decimal secondNumber, string symbol)
    {
        if (secondNumber == 0)
        {
            return firstNumber;
        }

        decimal? result;
        try
        {
            return symbol switch
                {
                    "+" => firstNumber + secondNumber,
                    "*" => firstNumber * secondNumber,
                    "-" => firstNumber - secondNumber,
                    "^" => Convert.ToDecimal(System.Math.Pow(Convert.ToDouble(firstNumber), Convert.ToDouble(secondNumber))),
                    _ => firstNumber / secondNumber
                };
        }
        catch (OverflowException)
        {
            result = 0;
        }

        return result;
    }
}
