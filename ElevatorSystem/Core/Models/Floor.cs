namespace ElevatorSystem.Core.Models
{
    public class Floor
    {
        public int Number { get; }
        public bool HasUpButton { get; }
        public bool HasDownButton { get; }
        public bool IsUpButtonPressed { get; private set; }
        public bool IsDownButtonPressed { get; private set; }

        public Floor(int number, bool hasUpButton, bool hasDownButton)
        {
            Number = number;
            HasUpButton = hasUpButton;
            HasDownButton = hasDownButton;
        }

        public void PressUpButton() => IsUpButtonPressed = true;
        public void PressDownButton() => IsDownButtonPressed = true;
        public void ClearUpButton() => IsUpButtonPressed = false;
        public void ClearDownButton() => IsDownButtonPressed = false;
    }
}
