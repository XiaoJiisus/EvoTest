public class ElevatorSvc
{
    private int CurrentFloor { get; set; } = (int)ElevatorFloors.MinFloor;

    public void RequestFloor(int floor)
    {
        if (floor < (int)ElevatorFloors.MinFloor || floor > (int)ElevatorFloors.MaxFloor)
            throw new ArgumentException("Invalid floor number");
        
        Move(floor);
    }

    public void Move(int floor)
    {
        CurrentFloor = floor;
    }

    public int GetElevatorFloor() => CurrentFloor;
}