using ElevatorSystem.Core.Enums;
using ElevatorSystem.Core.Models;

namespace ElevatorSystem.Core.Services
{
    public class ElevatorControlSystem
    {
        private readonly Elevator _elevator;
        private readonly List<Floor> _floors;

        public ElevatorControlSystem()
        {
            _elevator = new Elevator();
            _floors = new List<Floor>
            {
                new Floor(1, true, false),
                new Floor(2, true, true),
                new Floor(3, true, true),
                new Floor(4, true, true),
                new Floor(5, false, true)
            };
        }

        public Elevator GetElevator() => _elevator;
        public IReadOnlyList<Floor> GetFloors() => _floors.AsReadOnly();

        public void RequestElevator(int floor, Direction direction)
        {
            var requestedFloor = _floors[floor - 1];

            if (direction == Direction.Up)
                requestedFloor.PressUpButton();
            else if (direction == Direction.Down)
                requestedFloor.PressDownButton();

            _elevator.RequestFloor(floor);
        }

        public void RequestFloorFromInside(int floor)
        {
            _elevator.RequestFloor(floor);
        }

        public void Update()
        {
            _elevator.Move();

            if (_elevator.DoorState == DoorState.Open)
            {
                var currentFloor = _floors[_elevator.CurrentFloor - 1];
                currentFloor.ClearUpButton();
                currentFloor.ClearDownButton();
            }
        }
    }
}
