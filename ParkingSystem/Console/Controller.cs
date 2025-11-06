public class Controller
{
    public static bool Parking(ParkingLotService parkingLot, string[] parts)
    {
        if (parts.Length < 4)
        {
            Console.WriteLine("Usage: park <type> <registration> <color>");
            return false;
        }
        var vehicle = new Vehicle(parts[1].Trim(), parts[2].Trim(), parts[3].Trim());
        parkingLot.Park(vehicle);
        return true;
    }

    public static void PrintHelp()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine("  park <type> <registration> <color>   - Park a vehicle");
        Console.WriteLine("  leave <slotNumber>                   - Remove vehicle from slot");
        Console.WriteLine("  status                               - Show parking status");
        Console.WriteLine("  available                            - Show number of available slots");
        Console.WriteLine("  count <type>                         - Count vehicles of a type");
        Console.WriteLine("  color <color>                        - List registration numbers by color");
        Console.WriteLine("  odd                                  - List registration numbers with odd numeric suffix");
        Console.WriteLine("  even                                 - List registration numbers with even numeric suffix");
        Console.WriteLine("  help                                 - Show this help");
        Console.WriteLine("  exit                                 - Quit the app");
    }

    public static bool ConsoleApp(ParkingLotService parkingLot, string[] parts, string cmd)
    {
        switch (cmd)
        {
            case "park":
                // park <type> <regisNumber> <color>
                bool park = Parking(parkingLot, parts);
                if (!park)
                {
                    break;
                }
                break;

            case "leave":
                if (parts.Length < 2 || !int.TryParse(parts[1], out int slot))
                {
                    Console.WriteLine("Usage: leave <slotNumber>");
                    break;
                }
                parkingLot.Leave(slot);
                break;

            case "status":
                parkingLot.Status();
                break;

            case "available":
                Console.WriteLine($"Available slots: {parkingLot.AvailableSlot()}");
                break;

            case "count":
                if (parts.Length < 2)
                {
                    Console.WriteLine("Usage: count <vehicleType>");
                    break;
                }
                Console.WriteLine(parkingLot.TypeOfVehicle(parts[1].Trim()));
                break;

            case "color":
                if (parts.Length < 2)
                {
                    Console.WriteLine("Usage: color <color>");
                    break;
                }
                var regs = parkingLot.ColorVehicle(parts[1].Trim());
                Console.WriteLine(regs.Count > 0 ? string.Join(", ", regs) : "Not found");
                break;

            case "odd":
                var odd = parkingLot.OddRegist();
                Console.WriteLine(odd.Count > 0 ? string.Join(", ", odd) : "0");
                break;

            case "even":
                var even = parkingLot.EvenRegist();
                Console.WriteLine(even.Count > 0 ? string.Join(", ", even) : "0");
                break;

            case "help":
                PrintHelp();
                break;

            case "exit":
                return false;

            default:
                Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                break;
        }

        return true;
    }
    
    public static void MainApp()
    {
        Console.Write("Enter parking lot capacity: ");
		int capacity = Validation.PositiveValid();
        var parkingLot = new ParkingLot(capacity);
        var ParkingLotService = new ParkingLotService(parkingLot);

        PrintHelp();

		while (true)
        {
            Console.Write("$ ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var cmd = parts[0].ToLowerInvariant();

            bool flowControl = ConsoleApp(ParkingLotService, parts, cmd);
            if (!flowControl)
            {
                return;
            }
        }
    }
}