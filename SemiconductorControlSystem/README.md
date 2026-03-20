# Semiconductor Equipment Control System (WinForms Skeleton)

This project is a modular architecture for an industrial equipment control system, specifically designed for semiconductor manufacturing scenarios.

## 1. Project Structure

The system is split into three main projects to ensure separation of concerns and testability:

- **`SemiconductorControlSystem.Core`**: (Class Library - `.net8.0`)
  - Contains all business logic, interfaces, and models.
  - Independent of any UI framework, allowing it to be used in WinForms, WPF, or even as a Headless service.
  - Includes: `Interfaces/`, `Models/`, `Services/`.
- **`SemiconductorControlSystem`**: (WinForms Application - `.net8.0-windows`)
  - The presentation layer.
  - References `Core` and handles user interaction and realtime visualization.
- **`SemiconductorControlSystem.Tests`**: (xUnit Test Project - `.net8.0`)
  - Unit tests for the core logic.
  - Demonstrates how to test industrial services using `Moq`.

## 2. Key Features

- **Decoupled Architecture**: Logic is completely separated from the UI.
- **Dynamic Device Support**: Equipment is loaded from `config.json` and UI indicators are generated automatically.
- **Realtime Simulation**: Uses `Async/Await` and `Events` to simulate hardware behavior.
- **Manual Dependency Injection**: Services are injected into the Main Form via the constructor.

## 3. How to Build and Run

### Prerequisites
- .NET 8.0 SDK

### Build the entire solution
```bash
dotnet build SemiconductorControlSystem.sln
```

### Run Unit Tests
Since the logic is in a pure .NET library (`Core`), you can run tests on any platform (Windows, Linux, macOS):
```bash
dotnet test SemiconductorControlSystem.Tests/SemiconductorControlSystem.Tests.csproj
```

### Run the UI (Windows Only)
```bash
dotnet run --project SemiconductorControlSystem/SemiconductorControlSystem.csproj
```

## 4. Bonus: Real PLC & Device Integration

To transition to real hardware:
- **Modbus**: Implement `IPLCService` using `NModbus4`.
- **OPC UA**: Use `Opc.Ua.Client` to implement standard industrial communication.
- **SECS/GEM**: Implement semiconductor-specific protocols for host-to-equipment communication.

## 5. Future Expansions
- **Database Logging**: Write sensor data to InfluxDB or SQL Server.
- **Alarms System**: Centralized alarm handling and user notification.
- **Recipe Management**: Store manufacturing parameters in JSON or DB.
