using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SemiconductorControlSystem.Interfaces;
using SemiconductorControlSystem.Services;
using SemiconductorControlSystem.UI;

namespace SemiconductorControlSystem
{
    static class Program
    {
        /// <summary>
        /// Entry point for the WinForms application
        /// Implements Manual Dependency Injection
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Core Services Initialization
            ILoggerService logger = new LoggerService();
            IConfigService config = new ConfigService();
            config.Load();

            // 2. Hardware Layer Initialization
            IPLCService plcService = new MockPLCService(logger);
            ISensorService sensorService = new MockSensorService();

            // 3. Device List Initialization
            var devices = new List<IDeviceService>();
            foreach (var deviceName in config.Config.EnabledDevices)
            {
                devices.Add(new MockDeviceService(deviceName, logger));
            }

            // 4. Injecting Services into Main UI
            var mainForm = new MainForm(
                plcService,
                devices,
                sensorService,
                logger,
                config
            );

            // Start application loop
            Application.Run(mainForm);
        }
    }
}
