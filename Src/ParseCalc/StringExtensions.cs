using System.Text.RegularExpressions;

namespace ParseCalc;

public static partial class StringExtensions
{
    public static string[] SplitUp(this string text)
    {
        List<string> list = [];
        string line = string.Empty;
        foreach (char item in text.ToCharArray())
        {
            if (item.IsSymbol())
            {
                list.Add(line);
                line = string.Empty;
                list.Add(item.ToString());
            }
            else
            {
                line += item.ToString();
            }
        }

        if (!string.IsNullOrEmpty(line))
        {
            list.Add(line);
        }

        return list.ToArray();
    }

    public static bool RegexMatch(this string text, string pattern) =>
        Regex.IsMatch(text, pattern);

    [GeneratedRegex("[0-9]")]
    private static partial Regex _isNumberCheck();
    public static bool IsNumber(this string text) =>
        _isNumberCheck().IsMatch(text);

    [GeneratedRegex("[/*-+^]")]
    private static partial Regex _isSymbolCheck();
    public static bool IsSymbol(this string text) =>
        text.Length == 1
                ? _isSymbolCheck().IsMatch(text)
                : false;
}