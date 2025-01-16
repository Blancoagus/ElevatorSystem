using Xunit;
using ElevatorSystem.Core.Models;
using ElevatorSystem.Core.Enums;
using ElevatorSystem.Core.Services;


namespace ElevatorSystem.Tests
{
    public class FloorTests
    {
        [Theory]
        [InlineData(1, true, false)]  // First floor
        [InlineData(3, true, true)]   // Middle floor
        [InlineData(5, false, true)]  // Top floor
        public void Constructor_InitializesWithCorrectButtonConfiguration(
            int number, bool hasUpButton, bool hasDownButton)
        {
            // Arrange & Act
            var floor = new Floor(number, hasUpButton, hasDownButton);
            bool shouldHaveUpButton = number == 1;
            bool shouldHaveDownButton = number == 5;

            // Assert
            Assert.Equal(number, floor.Number);
            Assert.Equal(hasUpButton, floor.HasUpButton);
            Assert.Equal(hasDownButton, floor.HasDownButton);
            Assert.False(floor.IsUpButtonPressed);
            Assert.False(floor.IsDownButtonPressed);

        }

        [Fact]
        public void PressUpButton_SetsUpButtonState()
        {
            // Arrange
            var floor = new Floor(2, true, true);

            // Act
            floor.PressUpButton();

            // Assert
            Assert.True(floor.IsUpButtonPressed);
        }

        [Fact]
        public void PressDownButton_SetsDownButtonState()
        {
            // Arrange
            var floor = new Floor(2, true, true);

            // Act
            floor.PressDownButton();

            // Assert
            Assert.True(floor.IsDownButtonPressed);
        }

        [Fact]
        public void ClearButtons_ResetsButtonStates()
        {
            // Arrange
            var floor = new Floor(2, true, true);
            floor.PressUpButton();
            floor.PressDownButton();

            // Act
            floor.ClearUpButton();
            floor.ClearDownButton();

            // Assert
            Assert.False(floor.IsUpButtonPressed);
            Assert.False(floor.IsDownButtonPressed);
        }
    }
}