# Semiconductor Equipment Control System (WinForms Skeleton)

This project is a base architecture for an industrial equipment control system, specifically designed for semiconductor manufacturing scenarios (e.g., Robot Arms, Vacuum Chambers).

## 1. Project Architecture (Layered Architecture)

The system is divided into several layers to ensure separation of concerns and maintainability:

- **UI Layer (WinForms)**: Handles user interaction and realtime visualization.
- **Business/Service Layer**: Contains equipment logic, state management, and orchestration.
- **Data/Communication Layer**: Manages data flow from sensors and PLC communication.
- **Models/Common**: Defines shared entities like `DeviceStatus`, `SensorData`, and configuration models.

## 2. Folder Structure

- `/Interfaces`: Defines contracts for all services (`IPLCService`, `IDeviceService`, etc.).
- `/Services`: Contains concrete and mock implementations of the interfaces.
- `/Models`: Shared data classes and enumerations.
- `/UI`: WinForms forms and custom UI logic.
- `/Config`: System configuration (JSON based).

## 3. Key Features

- **Dynamic Device Support**: Equipment is loaded from a configuration file and UI indicators are generated automatically.
- **Realtime Simulation**: Uses `Async/Await` and `Events` to simulate PLC communication and sensor data streams.
- **Manual Dependency Injection**: Services are injected into the Main Form via the constructor, allowing for easy swapping of Mock vs. Real hardware services.
- **Logging**: Integrated log view with color-coded entries.

## 4. How to Build and Run

1. Open the project in **Visual Studio 2022** or **VS Code**.
2. Restore NuGet packages (requires `Newtonsoft.Json`).
3. Build the solution.
4. Run the executable.
   - Click **START SYSTEM** to initiate mock connection and start data sampling.
   - Click **STOP SYSTEM** to shutdown all components.

## 5. Bonus: Real PLC & Device Integration

To move from Mock to Production, you can implement the interfaces with real hardware libraries:

### PLC Integration (Modbus/OPC UA)
- **Modbus**: Use libraries like `NModbus4` or `FluentModbus`. Implement `IPLCService` by wrapping the Modbus TCP client.
- **OPC UA**: Use the `Opc.Ua.Fx` or `Opc.Ua.Client` libraries. This is standard for modern semiconductor equipment (SECS/GEM is also common).

### SECS/GEM
- For semiconductor-specific communication (Host to Equipment), look into libraries that implement the **SECS-II** and **GEM** standards (E4, E5, E30, E37).

## 6. Future Expansions

- **Database Logging**: Implement an `ILoggerService` that writes to SQL Server or InfluxDB for historical data.
- **Alarms System**: Add an `IAlarmService` to handle critical errors and provide user alerts.
- **Security**: Add role-based access control (RBAC) for different operator levels.
- **Recipe Management**: Store and load process parameters for different chip manufacturing steps.
- **Custom Controls**: Develop high-end GDI+ or WPF controls for better equipment visualization.
