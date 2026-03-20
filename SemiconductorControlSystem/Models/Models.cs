namespace SemiconductorControlSystem.Models
{
    public enum DeviceStatus
    {
        Idle,
        Running,
        Error,
        Disconnected
    }

    public class SensorData
    {
        public string SensorName { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    public class SystemConfig
    {
        public string PlcAddress { get; set; } = "127.0.0.1";
        public int PlcPort { get; set; } = 502;
        public int DataUpdateIntervalMs { get; set; } = 1000;
        public List<string> EnabledDevices { get; set; } = new List<string>();
    }
}
