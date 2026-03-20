using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SemiconductorControlSystem.Models;

namespace SemiconductorControlSystem.Interfaces
{
    /// <summary>
    /// Interface for PLC communication (e.g. Modbus, OPC UA)
    /// </summary>
    public interface IPLCService
    {
        bool IsConnected { get; }
        Task<bool> ConnectAsync(string address, int port);
        Task DisconnectAsync();
        Task<bool> WriteRegisterAsync(int address, short value);
        Task<short> ReadRegisterAsync(int address);
        event EventHandler<string> OnCommunicationError;
    }

    /// <summary>
    /// Interface for generic equipment control (Robot, Chamber, etc.)
    /// </summary>
    public interface IDeviceService
    {
        string DeviceName { get; }
        DeviceStatus Status { get; }
        Task StartAsync();
        Task StopAsync();
        Task ResetAsync();
        event EventHandler<DeviceStatus> OnStatusChanged;
    }

    /// <summary>
    /// Interface for data collection from sensors
    /// </summary>
    public interface ISensorService
    {
        Task<SensorData> GetLatestDataAsync(string sensorName);
        event EventHandler<SensorData> OnDataReceived;
        void StartSampling();
        void StopSampling();
    }

    /// <summary>
    /// Application logging service
    /// </summary>
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? ex = null);
        event EventHandler<string> OnLogAdded;
    }

    /// <summary>
    /// Configuration management service
    /// </summary>
    public interface IConfigService
    {
        SystemConfig Config { get; }
        void Load();
        void Save();
    }
}
