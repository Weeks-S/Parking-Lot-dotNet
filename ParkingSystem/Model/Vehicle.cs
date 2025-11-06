public class Vehicle
{
    public string Type { get; }
    public string RegisNumber { get; }
    public string Color { get; }

    public Vehicle(string type, string regisNumber, string color)
    {
        Type = type.Trim();
        RegisNumber = regisNumber.Trim().ToUpperInvariant();
        Color = color.Trim();
    }
}