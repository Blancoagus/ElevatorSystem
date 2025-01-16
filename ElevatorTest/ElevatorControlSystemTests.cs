using Xunit;
using ElevatorSystem.Core.Models;
using ElevatorSystem.Core.Enums;
using ElevatorSystem.Core.Services;


namespace ElevatorSystem.Tests
{

    public class ElevatorControlSystemTests
    {
        [Fact]
        public void Constructor_InitializesWithCorrectFloorConfiguration()
        {
            // Arrange & Act
            var system = new ElevatorControlSystem();
            var floors = system.GetFloors();

            // Assert
            Assert.Equal(5, floors.Count);
            Assert.True(floors[0].HasUpButton);
            Assert.False(floors[0].HasDownButton);
            Assert.True(floors[1].HasUpButton);
            Assert.True(floors[1].HasDownButton);
            Assert.False(floors[4].HasUpButton);
            Assert.True(floors[4].HasDownButton);
        }

        [Fact]
        public void RequestElevator_FromExterior_UpdatesFloorButtonAndElevatorQueue()
        {
            // Arrange
            var system = new ElevatorControlSystem();

            // Act
            system.RequestElevator(3, Direction.Up);

            // Assert
            var floors = system.GetFloors();
            Assert.True(floors[2].IsUpButtonPressed);
            Assert.Contains(3, system.GetElevator().RequestedFloors);
        }

        [Fact]
        public void RequestFloorFromInside_AddsToElevatorQueue()
        {
            // Arrange
            var system = new ElevatorControlSystem();

            // Act
            system.RequestFloorFromInside(4);

            // Assert
            Assert.Contains(4, system.GetElevator().RequestedFloors);
        }

        [Fact]
        public void Update_ClearsButtonsWhenElevatorArrives()
        {
            // Arrange
            var system = new ElevatorControlSystem();
            system.RequestElevator(2, Direction.Up);

            // Act
            system.Update(); // Move to floor 2

            // Assert
            var floors = system.GetFloors();
            Assert.False(floors[1].IsUpButtonPressed);
        }

        [Fact]
        public void Update_HandlesMultipleRequests()
        {
            // Arrange
            var system = new ElevatorControlSystem();
            system.RequestFloorFromInside(3);
            system.RequestFloorFromInside(4);

            // Act & Assert
            Assert.Equal(2, system.GetElevator().RequestedFloors.Count);

            // Move elevator
            system.Update();
            system.Update();

            // Should be at floor 3 and removed from queue
            Assert.Equal(3, system.GetElevator().CurrentFloor);
            Assert.Single(system.GetElevator().RequestedFloors);
            Assert.Contains(4, system.GetElevator().RequestedFloors);
        }
    }

}