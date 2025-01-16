using ElevatorSystem.Core.Enums;

namespace ElevatorSystem.Core.Models
{
    public class Elevator
    {
        public int CurrentFloor { get; private set; }
        public Direction Direction { get; private set; }
        public DoorState DoorState { get; private set; }
        public HashSet<int> RequestedFloors { get; } = new();

        public Elevator()
        {
            CurrentFloor = 1;
            Direction = Direction.None;
            DoorState = DoorState.Closed;
        }

        public void RequestFloor(int floor)
        {
            if (floor >= 1 && floor <= 5)
            {
                RequestedFloors.Add(floor);
                UpdateDirection();
            }
        }

        public void Move()
        {
            if (Direction == Direction.None || DoorState == DoorState.Open)
                return;

            CurrentFloor += Direction == Direction.Up ? 1 : -1;

            if (RequestedFloors.Contains(CurrentFloor))
            {
                RequestedFloors.Remove(CurrentFloor);
                OpenDoor();
            }

            UpdateDirection();
        }

        private void UpdateDirection()
        {
            if (!RequestedFloors.Any())
            {
                Direction = Direction.None;
                return;
            }

            var nextFloor = RequestedFloors.FirstOrDefault();
            Direction = nextFloor == CurrentFloor ? Direction.None
                                                : nextFloor > CurrentFloor
                                                ? Direction.Up : Direction.Down;
        }

        private void OpenDoor()
        {
            DoorState = DoorState.Open;
            Task.Delay(3000).ContinueWith(_ => CloseDoor());
        }

        private void CloseDoor()
        {
            DoorState = DoorState.Closed;
        }
    }
}
