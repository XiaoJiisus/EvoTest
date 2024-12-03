public enum ElevatorFloors
{
    MinFloor = 1,
    MaxFloor = 5
}

public class ElevatorVM
{
    public int MinFloor { get; set; } = (int)ElevatorFloors.MinFloor;
    public int MaxFloor { get; set; } = (int)ElevatorFloors.MaxFloor;
    public int CurrentFloor { get; set; } = 1;
    public int DestinationFloor { get; set; }
}