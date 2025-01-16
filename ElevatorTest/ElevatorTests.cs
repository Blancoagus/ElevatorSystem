using ElevatorSystem.Core.Models;
using ElevatorSystem.Core.Enums;


namespace ElevatorSystem.Tests
{
    public class ElevatorTests
    {
        [Fact]
        public void Constructor_InitializesWithCorrectDefaultValues()
        {
            // Arrange & Act
            var elevator = new Elevator();

            // Assert
            Assert.Equal(1, elevator.CurrentFloor);
            Assert.Equal(Direction.None, elevator.Direction);
            Assert.Equal(DoorState.Closed, elevator.DoorState);
            Assert.Empty(elevator.RequestedFloors);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void RequestFloor_ValidFloor_AddsToRequestedFloors(int floor)
        {
            // Arrange
            var elevator = new Elevator();

            // Act
            elevator.RequestFloor(floor);

            // Assert
            Assert.Contains(floor, elevator.RequestedFloors);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        [InlineData(-1)]
        public void RequestFloor_InvalidFloor_DoesNotAddToRequestedFloors(int floor)
        {
            // Arrange
            var elevator = new Elevator();

            // Act
            elevator.RequestFloor(floor);

            // Assert
            Assert.Empty(elevator.RequestedFloors);
        }

        [Fact]
        public void Move_WhenMovingUp_IncreasesFloor()
        {
            // Arrange
            var elevator = new Elevator();
            elevator.RequestFloor(3); 

            // Act
            elevator.Move();

            // Assert
            Assert.Equal(2, elevator.CurrentFloor);
            Assert.Equal(Direction.Up, elevator.Direction);
        }

        [Fact]
        public async void Move_WhenMovingDown_DecreasesFloor()
        {
            // Arrange
            var elevator = new Elevator();
            elevator.RequestFloor(3);

            // Move to floor 3
            elevator.Move();
            elevator.Move();

   
            await Task.Delay(3500); 

            // Now request floor 1
            elevator.RequestFloor(1);

            // Act
            elevator.Move();

            // Assert
            Assert.Equal(2, elevator.CurrentFloor);
            Assert.Equal(Direction.Down, elevator.Direction);
        }

        [Fact]
        public async Task Move_WhenReachingRequestedFloor_OpensDoor()
        {
            // Arrange
            var elevator = new Elevator();
            elevator.RequestFloor(2);

            // Act
            elevator.Move();

            // Assert
            Assert.Equal(2, elevator.CurrentFloor);
            Assert.Equal(DoorState.Open, elevator.DoorState);

            await Task.Delay(3500);
            Assert.Equal(DoorState.Closed, elevator.DoorState);
        }
    }

}