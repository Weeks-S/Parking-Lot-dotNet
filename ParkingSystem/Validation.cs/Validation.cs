using System.Text.RegularExpressions;
public class Validation
{
    public static int PositiveValid()
    {
        while (true)
        {
            var s = Console.ReadLine();
            if (int.TryParse(s, out int v) && v > 0)
                return v;
            Console.Write("Please enter a positive integer: ");
        }
    }

    public static bool IsValidIndonesianPlate(string? regis)
    {
        if (string.IsNullOrWhiteSpace(regis)) return false;
        var pattern = "^[A-Za-z]{1,2}[- ]?\\d{1,4}[- ]?[A-Za-z]{1,3}$";
        return Regex.IsMatch(regis.Trim(), pattern, RegexOptions.IgnoreCase);
    }
}