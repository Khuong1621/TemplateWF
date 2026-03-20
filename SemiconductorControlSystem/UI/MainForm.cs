using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SemiconductorControlSystem.Interfaces;
using SemiconductorControlSystem.Models;

namespace SemiconductorControlSystem.UI
{
    public partial class MainForm : Form
    {
        private readonly IPLCService _plcService;
        private readonly IEnumerable<IDeviceService> _deviceServices;
        private readonly ISensorService _sensorService;
        private readonly ILoggerService _logger;
        private readonly IConfigService _config;

        // Visual constants for the modern theme
        private static readonly Color ColorRunning = Color.FromArgb(46, 204, 113);
        private static readonly Color ColorError = Color.FromArgb(231, 76, 60);
        private static readonly Color ColorIdle = Color.FromArgb(52, 73, 94);
        private static readonly Color ColorText = Color.FromArgb(236, 240, 241);

        // Cache for dynamic UI controls to update status easily
        private readonly Dictionary<string, Panel> _deviceIndicatorMap = new Dictionary<string, Panel>();

        public MainForm(
            IPLCService plcService,
            IEnumerable<IDeviceService> deviceServices,
            ISensorService sensorService,
            ILoggerService logger,
            IConfigService config)
        {
            _plcService = plcService;
            _deviceServices = deviceServices;
            _sensorService = sensorService;
            _logger = logger;
            _config = config;

            InitializeComponent();
            SetupEvents();
            InitializeDynamicUI();
        }

        private void SetupEvents()
        {
            _logger.OnLogAdded += (s, msg) => AppendLog(msg);
            _sensorService.OnDataReceived += (s, data) => UpdateSensorUI(data);

            foreach (var device in _deviceServices)
            {
                device.OnStatusChanged += (s, status) => UpdateDeviceStatusUI(device.DeviceName, status);
            }
        }

        private void InitializeDynamicUI()
        {
            pnlDevices.Controls.Clear();
            _deviceIndicatorMap.Clear();

            foreach (var device in _deviceServices)
            {
                var pnl = new Panel
                {
                    Width = 140,
                    Height = 90,
                    BackColor = ColorIdle,
                    Margin = new Padding(8),
                    Padding = new Padding(2)
                };

                var lbl = new Label
                {
                    Text = device.DeviceName.Replace("_", " "),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = ColorText,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
                };

                pnl.Controls.Add(lbl);
                pnlDevices.Controls.Add(pnl);
                _deviceIndicatorMap[device.DeviceName] = pnl;
            }
        }

        private void AppendLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => AppendLog(message)));
                return;
            }
            txtLog.AppendText(message + Environment.NewLine);
        }

        private void UpdateSensorUI(SensorData data)
        {
            if (lblSensorValue.InvokeRequired)
            {
                lblSensorValue.Invoke(new Action(() => UpdateSensorUI(data)));
                return;
            }
            lblSensorValue.Text = $"{data.Value:F1} {data.Unit}";
            lblSensorTime.Text = $"Last data received: {data.Timestamp:HH:mm:ss}";
        }

        private void UpdateDeviceStatusUI(string deviceName, DeviceStatus status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDeviceStatusUI(deviceName, status)));
                return;
            }

            if (_deviceIndicatorMap.TryGetValue(deviceName, out var pnl))
            {
                pnl.BackColor = status switch
                {
                    DeviceStatus.Running => ColorRunning,
                    DeviceStatus.Error => ColorError,
                    DeviceStatus.Idle => ColorIdle,
                    _ => Color.FromArgb(45, 52, 54)
                };
            }
            _logger.LogInfo($"[STATE] {deviceName} -> {status}");
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            _logger.LogInfo("System booting up...");

            try
            {
                await _plcService.ConnectAsync(_config.Config.PlcAddress, _config.Config.PlcPort);

                foreach (var device in _deviceServices)
                {
                    await device.StartAsync();
                }

                _sensorService.StartSampling();
                btnStop.Enabled = true;
                _logger.LogInfo("STATUS: ONLINE - All systems functional.");
            }
            catch (Exception ex)
            {
                _logger.LogError("SYSTEM FAULT during startup", ex);
                btnStart.Enabled = true;
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            _logger.LogInfo("Initiating system shutdown sequence...");

            _sensorService.StopSampling();

            foreach (var device in _deviceServices)
            {
                await device.StopAsync();
            }

            await _plcService.DisconnectAsync();
            btnStart.Enabled = true;
            _logger.LogInfo("STATUS: OFFLINE - Safe shutdown completed.");
        }
    }
}
