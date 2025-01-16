using ElevatorSystem.Core.Enums;
using ElevatorSystem.Core.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var controlSystem = new ElevatorControlSystem();
        var running = true;

        // This will simulate the continous movement of the elevator
        _ = Task.Run(async () =>
        {
            while (running)
            {
                controlSystem.Update();
                await Task.Delay(3000); // Simulates movement
            }
        });

        Console.WriteLine("=== Elevator System ===");

        while (running)
        {
            DrawElevatorStatus(controlSystem);
            ShowMenu();

            var key = Console.ReadKey(true);
            Console.Clear();

            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    HandleFloorRequest(controlSystem);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    HandleElevatorCallFromFloor(controlSystem);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    break;
                case ConsoleKey.Q:
                    running = false;
                    break;
                default:
                    HandleIncorrectKey();
                    break;
            }
        }
    }

    private static void HandleIncorrectKey()
    {
        Console.WriteLine("Please, select a correct option");
    }
    private static void DrawElevatorStatus(ElevatorControlSystem controlSystem)
    {
        var elevator = controlSystem.GetElevator();
        var floors = controlSystem.GetFloors();

        Console.WriteLine("\n=== Elevator status ===");
        Console.WriteLine($"Actual floor: {elevator.CurrentFloor}");
        Console.WriteLine($"Direction: {elevator.Direction}");
        Console.WriteLine($"Door status: {elevator.DoorState}");
        Console.WriteLine($"Requested floors: {string.Join(", ", elevator.RequestedFloors.OrderBy(f => f))}");

        Console.WriteLine("\n=== Floors status ===");
        for (int i = floors.Count(); i >= 1; i--)
        {
            var floor = floors[i - 1];
            var isCurrentFloor = elevator.CurrentFloor == i;
            var floorMarker = isCurrentFloor ? "[E]" : "   ";

            Console.Write($"Floor {i} {floorMarker}");

            Console.WriteLine();
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("\n=== Menu ===");
        Console.WriteLine("1. Request floor from inside the elevator");
        Console.WriteLine("2. Call elevator from a particular floor");
        Console.WriteLine("3. Refresh (updates elevator status and floor)");
        Console.WriteLine("Q. Exit");
        Console.Write("\nSelect an option: ");
    }

    private static void HandleFloorRequest(ElevatorControlSystem controlSystem)
    {
        Console.Write("\nSelect the floor number (1-5): ");
        if (int.TryParse(Console.ReadLine(), out int floor) && floor >= 1 && floor <= 5)
        {
            if (floor == controlSystem.GetElevator().CurrentFloor)
                Console.WriteLine("You select the floor you are on");
            else
            {
                controlSystem.RequestFloorFromInside(floor);
                Console.WriteLine($"Requesting floor {floor}");
            }

        }
        else
        {
            Console.WriteLine("You select an invalid floor");
        }
    }

    private static void HandleElevatorCallFromFloor(ElevatorControlSystem controlSystem)
    {
        Console.Write("\nSelect the floor number (1-5): ");
        if (int.TryParse(Console.ReadLine(), out int floor) && floor >= 1 && floor <= 5)
        {
            if (floor == controlSystem.GetElevator().CurrentFloor)
                Console.WriteLine("You select the floor you are on");
            else
            {
                var floors = controlSystem.GetFloors();
                var currentFloor = floors[floor - 1];

                if (floor == 1)
                {
                    controlSystem.RequestElevator(floor, Direction.Up);
                    Console.WriteLine($"Calling elevator to floor {floor}");
                }
                else if (floor == 5)
                {
                    controlSystem.RequestElevator(floor, Direction.Down);
                    Console.WriteLine($"Calling elevator to floor {floor}");
                }
                else
                {
                    Console.Write("¿Go up or down? (U: Up, D: Down): ");
                    var dirKey = Console.ReadKey();
                    Console.WriteLine();

                    if (dirKey.Key == ConsoleKey.U)
                    {
                        controlSystem.RequestElevator(floor, Direction.Up);
                        Console.WriteLine($"Calling elevator to floor {floor} (Up)");
                    }
                    else if (dirKey.Key == ConsoleKey.D)
                    {
                        controlSystem.RequestElevator(floor, Direction.Down);
                        Console.WriteLine($"Calling elevator to floor {floor} (Down)");
                    }
                    else
                    {
                        Console.WriteLine("Invalid direction");
                    }
                }
            }

        }
        else
        {
            Console.WriteLine("Invalid floor");
        }
    }
}
