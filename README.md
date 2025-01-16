# 🛗 Elevator Control System

## 📝 Description
A simulation system for an elevator implemented in C# (.NET 8). The system simulates a 5-floor elevator with functionality for internal and external calls.

## 🌟 Features
- Real-time simulation of elevator movement
- Control system for 5 floors
- Handling of internal and external calls
- Console-based status visualization
- Comprehensive unit tests
- Object-oriented design

## 🏗️ Architecture

### Main Components
- **Elevator**: Manages the elevator's state and movement
- **Floor**: Represents each floor and its buttons
- **ElevatorControlSystem**: Coordinates the interaction between floors and the elevator

### Class Diagram
```
┌─────────────┐     ┌──────────────────────┐
│   Floor     │     │    ElevatorControl   │
│             │     │       System         │
└─────────────┘     └──────────────────────┘
                              │
                     ┌──────────────────┐
                     │     Elevator     │
                     └──────────────────┘
```

### Test Coverage
- System initialization
- Elevator movement
- Request handling
- Button states
- Edge cases

## 🎮 Usage

### Console Interface
```
=== Elevator Status ===
Current Floor: 1
Direction: None
Door State: Closed

=== Floor Status ===
Floor 5    
Floor 4    
Floor 3    
Floor 2    
Floor 1 [E]

=== Menu ===
1. Request a floor from inside the elevator
2. Call the elevator from a floor
3. Refresh (updates elevator status and floor)
Q. Quit
```

### Available Commands
- **1**: Request a floor from inside the elevator
- **2**: Call the elevator from a specific floor
- **3**: Refresh (updates elevator status and floor)
- **Q**: Exit the application

## 🔧 Configuration
The system is preconfigured with:
- 5 floors
- 3-second door wait time
- 3-second interval between movements

## 📚 Project Structure
```
ElevatorSystem/
├── Core/
│   ├── Models/
│   │   ├── Elevator.cs
│   │   └── Floor.cs
│   ├── Enums/
│   │   ├── Direction.cs
│   │   └── DoorState.cs
│   └── Services/
│       └── ElevatorControlSystem.cs
├── Program.cs
│
ElevatorTest
    ├── ElevatorTests.cs
    ├── FloorTests.cs
    └── ElevatorControlSystemTests.cs
```


## 🛠️ Areas for Improvement
- [ ] Implementation of interfaces for functionalities
- [ ] Graphical interface using Blazor
- [ ] Dynamic configuration of the number of floors
- [ ] Simulation of maximum weight capacity
- [ ] Emergency system
- [ ] Event logging and statistics

## 👥 Authors
* **Agustin Blanco** - [Blancoagus](https://github.com/Blancoagus/)

