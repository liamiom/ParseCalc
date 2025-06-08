namespace ParseCalc;

public static class DecimalExtensions
{
    public static string ToFormat(this decimal _decimal) => 
        _decimal.ToString("###,###.#####################################");

    public static string ToShortFormat(this decimal _decimal) => 
        _decimal.ToString();

    public static int LeftPadding(this decimal _decimal)
    {
        int left = 5 - System.Math.Round(_decimal, 0).ToString().Length;
        left *= 8;

        return left;
    }

    public static string ToInt(this decimal _decimal) => 
        _decimal.ToString("###,###,###.");

    public static string ToDecimals(this decimal _decimal) => 
        (_decimal - decimal.Floor(_decimal)).ToString(".######");
}