using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SemiconductorControlSystem.Interfaces;
using SemiconductorControlSystem.Models;

namespace SemiconductorControlSystem.Services
{
    /// <summary>
    /// Mock implementation of PLC Service for testing and simulation
    /// </summary>
    public class MockPLCService : IPLCService
    {
        public bool IsConnected { get; private set; }
        public event EventHandler<string>? OnCommunicationError;
        private readonly ILoggerService _logger;

        public MockPLCService(ILoggerService logger)
        {
            _logger = logger;
        }

        public async Task<bool> ConnectAsync(string address, int port)
        {
            _logger.LogInfo($"Connecting to PLC at {address}:{port}...");
            await Task.Delay(500); // Simulate connection delay
            IsConnected = true;
            _logger.LogInfo("PLC connected successfully (Mock).");
            return true;
        }

        public async Task DisconnectAsync()
        {
            _logger.LogInfo("Disconnecting from PLC...");
            await Task.Delay(200);
            IsConnected = false;
            _logger.LogInfo("PLC disconnected.");
        }

        public async Task<short> ReadRegisterAsync(int address)
        {
            if (!IsConnected) throw new InvalidOperationException("PLC not connected");
            await Task.Delay(10);
            return (short)new Random().Next(0, 100);
        }

        public async Task<bool> WriteRegisterAsync(int address, short value)
        {
            if (!IsConnected) throw new InvalidOperationException("PLC not connected");
            _logger.LogInfo($"PLC Write: Register {address} = {value}");
            await Task.Delay(20);
            return true;
        }
    }

    /// <summary>
    /// Mock Device Service for simulating generic equipment
    /// </summary>
    public class MockDeviceService : IDeviceService
    {
        public string DeviceName { get; }
        public DeviceStatus Status { get; private set; } = DeviceStatus.Idle;
        public event EventHandler<DeviceStatus>? OnStatusChanged;
        private readonly ILoggerService _logger;

        public MockDeviceService(string name, ILoggerService logger)
        {
            DeviceName = name;
            _logger = logger;
        }

        public async Task StartAsync()
        {
            _logger.LogInfo($"Starting device {DeviceName}...");
            await Task.Delay(200);
            Status = DeviceStatus.Running;
            OnStatusChanged?.Invoke(this, Status);
        }

        public async Task StopAsync()
        {
            _logger.LogInfo($"Stopping device {DeviceName}...");
            await Task.Delay(200);
            Status = DeviceStatus.Idle;
            OnStatusChanged?.Invoke(this, Status);
        }

        public async Task ResetAsync()
        {
            _logger.LogInfo($"Resetting device {DeviceName}...");
            await Task.Delay(200);
            Status = DeviceStatus.Idle;
            OnStatusChanged?.Invoke(this, Status);
        }
    }

    /// <summary>
    /// Mock Sensor Service to provide simulated realtime data
    /// </summary>
    public class MockSensorService : ISensorService
    {
        public event EventHandler<SensorData>? OnDataReceived;
        private bool _isSampling;
        private readonly object _lock = new object();
        private readonly Random _random = new Random();

        public async Task<SensorData> GetLatestDataAsync(string sensorName)
        {
            return await Task.Run(() => new SensorData
            {
                SensorName = sensorName,
                Value = Math.Round(_random.NextDouble() * 100, 2),
                Unit = "C",
                Timestamp = DateTime.Now
            });
        }

        public void StartSampling()
        {
            lock (_lock)
            {
                if (_isSampling) return; // Prevent multiple sampling loops
                _isSampling = true;
            }

            Task.Run(async () =>
            {
                while (true)
                {
                    lock (_lock)
                    {
                        if (!_isSampling) break;
                    }
                    var data = await GetLatestDataAsync("Chamber_Temp_1");
                    OnDataReceived?.Invoke(this, data);
                    await Task.Delay(1000);
                }
            });
        }

        public void StopSampling()
        {
            lock (_lock)
            {
                _isSampling = false;
            }
        }
    }

    /// <summary>
    /// Basic logging implementation
    /// </summary>
    public class LoggerService : ILoggerService
    {
        public event EventHandler<string>? OnLogAdded;

        public void LogInfo(string message) => Log("INFO", message);
        public void LogWarning(string message) => Log("WARN", message);
        public void LogError(string message, Exception? ex = null)
            => Log("ERROR", $"{message} {(ex != null ? ex.Message : "")}");

        private void Log(string level, string message)
        {
            string logEntry = $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}";
            OnLogAdded?.Invoke(this, logEntry);
        }
    }

    /// <summary>
    /// JSON-based configuration management
    /// </summary>
    public class ConfigService : IConfigService
    {
        public SystemConfig Config { get; private set; } = new SystemConfig();
        private const string ConfigPath = "config.json";

        public void Load()
        {
            try
            {
                if (System.IO.File.Exists(ConfigPath))
                {
                    string json = System.IO.File.ReadAllText(ConfigPath);
                    Config = Newtonsoft.Json.JsonConvert.DeserializeObject<SystemConfig>(json) ?? new SystemConfig();
                }
                else
                {
                    Config = new SystemConfig
                    {
                        EnabledDevices = new List<string> { "Robot_Arm_1", "Vacuum_Chamber_1", "Load_Lock_A" }
                    };
                    Save();
                }
            }
            catch
            {
                Config = new SystemConfig();
            }
        }

        public void Save()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Config, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(ConfigPath, json);
        }
    }
}
