public class ParkingLot
{
    private readonly int slot;
    private List<(int SlotNumber, Vehicle? Vehicle, DateTime? CheckIn)> lot = [];
    
    public List<(int SlotNumber, Vehicle? Vehicle, DateTime? CheckIn)> Lot
    {
        get { return lot; }
    }
    public ParkingLot(int slot)
    {
        this.slot = slot;
        InitializeLot(slot);
    }

    private void InitializeLot(int slot)
    {
        for (int i = 1; i <= slot; i++)
        {
            lot.Add((i, null, null));
        }
    }



}