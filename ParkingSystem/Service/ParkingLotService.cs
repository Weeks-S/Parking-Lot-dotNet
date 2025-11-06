using System.Text.RegularExpressions;
using ConsoleTables;

public partial class ParkingLotService
{

    public ParkingLot parkingLot { get; }
    public List<(int SlotNumber, Vehicle? Vehicle, DateTime? CheckIn)> lot = [];
    public ParkingLotService(ParkingLot parkingLot)
    {
        this.parkingLot = parkingLot;
        this.lot = parkingLot.Lot;
    }
    public void Park(Vehicle vehicle)
    {
        if (vehicle == null)
        {
            Console.WriteLine("Invalid vehicle");
            return;
        }

        if (!Validation.IsValidIndonesianPlate(vehicle.RegisNumber))
        {
            Console.WriteLine("Invalid registration number.( B-1234-CD)");
            return;
        }

        for (int i = 0; i < lot.Count; i++)
        {
            if (lot[i].Vehicle == null)
            {
                lot[i] = (lot[i].SlotNumber, vehicle, DateTime.Now);
                Console.WriteLine($"Allocated slot number {lot[i].SlotNumber}");
                return;
            }
        }

        Console.WriteLine("Sorry, parking lot is full");
    }

    public void Status()
    {
        var table = new ConsoleTable("Slot", "Type", "Registration No", "Color");
        foreach (var item in lot)
        {
            table.AddRow(item.SlotNumber, item.Vehicle?.Type ?? "-", item.Vehicle?.RegisNumber ?? "-", item.Vehicle?.Color ?? "-");
        }
        table.Write(Format.Alternative);
    }

    public int TypeOfVehicle(string type)
    {
        if (string.IsNullOrWhiteSpace(type)) return 0;
        var normalizedType = type.Trim().ToLowerInvariant();
        return lot.Count(slot =>
            slot.Vehicle != null && slot.Vehicle.Type.Trim().ToLowerInvariant() == normalizedType);
    }

    public List<string> OddRegist()
    {
        var regisNum = new List<string>();
        foreach (var v in lot)
        {
            string? regis = v.Vehicle?.RegisNumber?.Trim();
            if (!string.IsNullOrEmpty(regis))
            {
                var m = MyRegex().Match(regis);
                if (m.Success && int.TryParse(m.Value, out int num) && num % 2 != 0)
                    regisNum.Add(regis);
            }
        }
        return regisNum;
    }

    public List<string> EvenRegist()
    {
        var regisNum = new List<string>();
        foreach (var v in lot)
        {
            string? regis = v.Vehicle?.RegisNumber?.Trim();
            if (!string.IsNullOrEmpty(regis))
            {
                var m = MyRegex1().Match(regis);
                if (m.Success && int.TryParse(m.Value, out int num) && num % 2 == 0)
                    regisNum.Add(regis);
            }
        }
        return regisNum;
    }

    public void Leave(int slot)
    {
        if (slot < 1 || slot > lot.Count)
        {
            Console.WriteLine($"Invalid Slot Number: {slot}");
        }
        if (lot[slot - 1].Vehicle != null)
        {
            DateTime? parkTime = lot[slot - 1].CheckIn;
            if (parkTime == null)
            {
                Console.WriteLine("Error: Check-in time not found");
                return;
            }
            DateTime checkIn = parkTime.Value;
            int fee = CalculateFee(checkIn);
            lot[slot - 1] = (slot, null, null);
            Console.WriteLine($"Your fee will be{fee}, Slot number {slot} is free");
        }
        else
        {
            Console.WriteLine($"No Vehicle Parked in slot {slot}");
        }
    }

    public int AvailableSlot()
    {
        return lot.Count(s => s.Vehicle == null);
    }

    public List<string> ColorVehicle(string color)
    {
        if (string.IsNullOrWhiteSpace(color)) return new List<string>();
        var target = color.Trim();
        return lot
            .Where(s => s.Vehicle != null && string.Equals(s.Vehicle.Color?.Trim(), target, StringComparison.OrdinalIgnoreCase))
            .Select(s => s.Vehicle!.RegisNumber)
            .ToList();
    }

    private int CalculateFee(DateTime checkIn)
    {
        DateTime checkOut = DateTime.Now;
        int duration = (int)Math.Ceiling((checkOut - checkIn).TotalHours);
        int firstHour = 3000;
        int fee = 2000;
        if (duration <= 1) return firstHour;
        return firstHour + fee * (duration - 1);
    }

    [GeneratedRegex("\\d+")]
    private static partial Regex MyRegex();
    [GeneratedRegex("\\d+")]
    private static partial Regex MyRegex1();
}

public class List<T1, T2, T3>
{
}