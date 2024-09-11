using AzureIoTHubLogging.Controllers;
using AzureIoTHubLogging.Hardware;
using AzureIoTHubLogging.Models;
using Meadow;
using Meadow.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureIoTHubLogging;

public class MainController
{
    private IAzureIoTHubLoggingHardware? hardware;
    private SensorController sensorController;
    private DisplayController displayController;
    private InputController inputController;
    private IoTHubMqttController iotHubController;

    private int currentGraphType = 0;

    private List<double> temperatureReadings = new List<double>();
    private List<double> pressureReadings = new List<double>();
    private List<double> humidityReadings = new List<double>();

    public MainController(IAzureIoTHubLoggingHardware hardware)
    {
        this.hardware = hardware;
    }

    public async Task Initialize()
    {
        sensorController = new SensorController(hardware);
        sensorController.Updated += SensorControllerUpdated;

        var cloudLogger = new CloudLogger();
        Resolver.Log.AddProvider(cloudLogger);
        Resolver.Services.Add(cloudLogger);

        inputController = new InputController(hardware);
        inputController.LeftButtonPressed += LeftButtonPressed;
        inputController.RightButtonPressed += RightButtonPressed;

        displayController = new DisplayController(hardware.Display);
        displayController.ShowSplashScreen();
        Thread.Sleep(3000);
        displayController.ShowDataScreen();

        iotHubController = new IoTHubMqttController();
        await InitializeIoTHub();
    }

    private async Task InitializeIoTHub()
    {
        while (!iotHubController.isAuthenticated)
        {
            displayController.UpdateWiFiStatus(hardware.NetworkAdapter.IsConnected);

            if (hardware.NetworkAdapter.IsConnected)
            {
                displayController.UpdateStatus("Authenticating...");

                bool authenticated = await iotHubController.Initialize();

                if (authenticated)
                {
                    displayController.UpdateStatus("Authenticated");
                    await Task.Delay(2000);
                    displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));
                }
                else
                {
                    displayController.UpdateStatus("Not Authenticated");
                }
            }
            else
            {
                displayController.UpdateStatus("Offline");
            }

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }

    private async void SensorControllerUpdated(object sender, AtmosphericConditions e)
    {
        displayController.UpdateWiFiStatus(hardware.NetworkAdapter.IsConnected);

        temperatureReadings.Add(e.Temperature.Celsius);
        if (temperatureReadings.Count > 10)
        {
            temperatureReadings.RemoveAt(0);
        }

        pressureReadings.Add(e.Pressure.Millibar);
        if (pressureReadings.Count > 10)
        {
            pressureReadings.RemoveAt(0);
        }

        humidityReadings.Add(e.Humidity.Percent);
        if (humidityReadings.Count > 10)
        {
            humidityReadings.RemoveAt(0);
        }

        if (hardware.NetworkAdapter.IsConnected)
        {
            displayController.UpdateSyncStatus(true);
            displayController.UpdateStatus("Sending data...");
            Thread.Sleep(2000);

            await iotHubController.SendEnvironmentalReading(e.Temperature, e.Humidity, e.Pressure);

            displayController.UpdateStatus("Data sent!");
            Thread.Sleep(2000);
            displayController.UpdateSyncStatus(false);

            displayController.UpdateLatestReading(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));

            UpdateGraph();
        }
        else
        {
            displayController.UpdateStatus("Offline...");
        }

        displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));
    }

    private void LeftButtonPressed(object sender, EventArgs e)
    {
        currentGraphType = currentGraphType - 1 < 0 ? 2 : currentGraphType - 1;

        UpdateGraph();
    }
    private void RightButtonPressed(object sender, EventArgs e)
    {
        currentGraphType = currentGraphType + 1 > 2 ? 0 : currentGraphType + 1;

        UpdateGraph();
    }

    private void UpdateGraph()
    {
        switch (currentGraphType)
        {
            case 0:
                displayController.UpdateGraph(currentGraphType, temperatureReadings);
                break;
            case 1:
                displayController.UpdateGraph(currentGraphType, pressureReadings);
                break;
            case 2:
                displayController.UpdateGraph(currentGraphType, humidityReadings);
                break;
        }
    }

    public Task Run()
    {
        _ = sensorController.StartUpdating(TimeSpan.FromSeconds(15));

        return Task.CompletedTask;
    }
}