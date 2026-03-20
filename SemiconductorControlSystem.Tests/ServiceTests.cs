using Xunit;
using Moq;
using SemiconductorControlSystem.Interfaces;
using SemiconductorControlSystem.Services;
using SemiconductorControlSystem.Models;
using System.Threading.Tasks;

namespace SemiconductorControlSystem.Tests
{
    public class ServiceTests
    {
        [Fact]
        public async Task MockPLCService_Connect_SetsIsConnectedToTrue()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerService>();
            var plcService = new MockPLCService(mockLogger.Object);

            // Act
            bool result = await plcService.ConnectAsync("127.0.0.1", 502);

            // Assert
            Assert.True(result);
            Assert.True(plcService.IsConnected);
            mockLogger.Verify(l => l.LogInfo(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task MockDeviceService_Start_UpdatesStatusToRunning()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerService>();
            var deviceService = new MockDeviceService("Test_Robot", mockLogger.Object);
            DeviceStatus updatedStatus = DeviceStatus.Idle;
            deviceService.OnStatusChanged += (s, status) => updatedStatus = status;

            // Act
            await deviceService.StartAsync();

            // Assert
            Assert.Equal(DeviceStatus.Running, deviceService.Status);
            Assert.Equal(DeviceStatus.Running, updatedStatus);
        }

        [Fact]
        public async Task MockSensorService_GetLatestData_ReturnsValidData()
        {
            // Arrange
            var sensorService = new MockSensorService();

            // Act
            var data = await sensorService.GetLatestDataAsync("Test_Sensor");

            // Assert
            Assert.NotNull(data);
            Assert.Equal("Test_Sensor", data.SensorName);
            Assert.Equal("C", data.Unit);
        }
    }
}
