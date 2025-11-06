# ParkingSystem (Console)

A simple .NET console application that simulates a parking lot. It stores vehicle information (type, registration number, color), allows parking and leaving, shows status, and provides searches such as color and odd/even registration checks.

## Features
- Interactive console REPL to park and remove vehicles.
- Status table (uses ConsoleTables) showing slot, type, registration, and color.
- Input normalization: registration numbers are trimmed and uppercased; other fields are trimmed.
- Fee calculation on leaving (first hour + per-hour fee).

## Build
From PowerShell, in the repository root or project directory:

```powershell
cd "C:\Users\Project\ParkingSystem"

dotnet build
```

## Run
Start the interactive console app:

```powershell
# From the project dir
dotnet run

# Or from anywhere
# dotnet run --project "C:\Users\Project\ParkingSystem\ParkingSystem.csproj"
```

When the app starts, you will be prompted for the parking lot capacity (positive integer). After that, use the commands described below.

## Commands
- park <type> <registration> <color>
  - Example: `park car B-1234-CD red`
  - Note: registration is validated against a permissive Indonesian plate regex (e.g., `B-1234-CD`). Registration is stored in upper case.
- leave <slotNumber>
  - Example: `leave 1`
- status
  - Prints a formatted table of slots and vehicles.
- available
  - Prints the number of available slots.
- count <type>
  - Counts parked vehicles by type (case-insensitive).
- color <color>
  - Lists registration numbers that match the color (case-insensitive, trimmed).
- odd
  - Lists registration numbers whose numeric portion is odd.
- even
  - Lists registration numbers whose numeric portion is even.
- help
  - Prints the help text.
- exit
  - Quit the application.

## Input trimming and normalization
- All input arguments are trimmed before being stored or compared.
- Registration numbers are stored uppercased (e.g., `b-1234-cd` -> `B-1234-CD`).

## Validation
- The registration validator accepts common Indonesian formats using the pattern: `^[A-Za-z]{1,2}[- ]?\d{1,4}[- ]?[A-Za-z]{1,3}$`.
- If you need stricter validation (province-specific rules, exact suffix lengths), the regex can be tightened.

## Next steps (optional)
- Add unit tests for `ParkingLot` behavior.
- Support quoted arguments in the REPL (so tokens with spaces can be passed without manual quoting).
- Normalize registration separators to a canonical display format.
