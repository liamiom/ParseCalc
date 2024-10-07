namespace ParseCalc;

public static class CharExtensions
{
    public static bool IsNumber(this char Char) => 
        IsInString(Char, "0123456789.");

    public static bool IsSymbol(this char Char) => 
        IsInString(Char, "/*-+^");

    public static bool IsInString(char Item, string SearchString) => 
        SearchString
        .ToCharArray()
        .Any(i => i == Item);
}