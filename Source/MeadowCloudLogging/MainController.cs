﻿using Meadow;
using Meadow.Hardware;
using Meadow.Logging;
using MeadowCloudLogging.Controllers;
using MeadowCloudLogging.Hardware;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeadowCloudLogging;

internal class MainController
{
    IMeadowCloudLoggingHardware hardware;
    IWiFiNetworkAdapter network;
    DisplayController displayController;

    public MainController(IMeadowCloudLoggingHardware hardware, IWiFiNetworkAdapter network)
    {
        this.hardware = hardware;
        this.network = network;
    }

    public void Initialize()
    {
        hardware.Initialize();

        var cloudLogger = new CloudLogger();
        Resolver.Log.AddProvider(cloudLogger);
        Resolver.Services.Add(cloudLogger);

        displayController = new DisplayController(hardware.Display);
        displayController.ShowSplashScreen();
        Thread.Sleep(3000);
        displayController.ShowDataScreen();
    }

    private void RecordSensor()
    {
        if (hardware.TemperatureSensor.Temperature == null ||
            hardware.BarometricPressureSensor.Pressure == null ||
            hardware.HumiditySensor.Humidity == null)
        {
            return;
        }

        displayController.UpdateAtmosphericConditions(
            temperature: $"{hardware.TemperatureSensor.Temperature.Value.Celsius:N0}",
            pressure: $"{hardware.BarometricPressureSensor.Pressure.Value.Millibar:N0}",
            humidity: $"{hardware.HumiditySensor.Humidity.Value.Percent:N0}");

        if (network.IsConnected)
        {
            displayController.UpdateSyncStatus(true);
            displayController.UpdateStatus("Sending data...");
            Thread.Sleep(2000);

            var cloudLogger = Resolver.Services.Get<CloudLogger>();
            cloudLogger?.LogEvent(1000, "environment reading", new Dictionary<string, object>()
            {
                { "temperature", $"{hardware.TemperatureSensor.Temperature.Value.Celsius:N2}" },
                { "pressure", $"{hardware.BarometricPressureSensor.Pressure.Value.Millibar:N2}" },
                { "humidity", $"{hardware.HumiditySensor.Humidity.Value.Percent:N2}" },
            });

            displayController.UpdateStatus("Data sent!");
            Thread.Sleep(2000);
            displayController.UpdateSyncStatus(false);
            displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));

            displayController.UpdateLastUpdated(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));
        }
        else
        {
            displayController.UpdateStatus("Offline...");
        }
    }

    public async Task Run()
    {
        hardware.TemperatureSensor.StartUpdating(TimeSpan.FromMinutes(1));
        hardware.BarometricPressureSensor.StartUpdating(TimeSpan.FromMinutes(1));
        hardware.HumiditySensor.StartUpdating(TimeSpan.FromMinutes(1));

        while (true)
        {
            displayController.UpdateWiFiStatus(network.IsConnected);

            if (network.IsConnected)
            {
                displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));

                RecordSensor();

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
            else
            {
                displayController.UpdateStatus("Offline...");

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}