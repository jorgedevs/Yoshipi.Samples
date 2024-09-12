using Meadow;
using Meadow.Foundation.Sensors.Temperature;
using Meadow.Logging;
using Meadow.Peripherals.Displays;
using Meadow.Units;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YoshiPi;

namespace TankTemperatureMonitor;

public class MeadowApp : YoshiPiApp
{
    private DisplayController? displayController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        var cloudLogger = new CloudLogger();
        Resolver.Log.AddProvider(cloudLogger);
        Resolver.Services.Add(cloudLogger);

        Hardware.Display.InvertDisplayColor(true);
        displayController = new DisplayController((IPixelDisplay)Hardware.Display);

        var temperatureSensor = new Mcp9601(Hardware.Qwiic);
        temperatureSensor.Updated += TemperatureSensorUpdated;
        temperatureSensor.StartUpdating(TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private void TemperatureSensorUpdated(object? sender, IChangeResult<(Temperature? TemperatureHot, Temperature? TemperatureCold)> e)
    {
        displayController.UpdateStatus("Sending data...");
        Thread.Sleep(2000);

        var cloudLogger = Resolver.Services.Get<CloudLogger>();
        cloudLogger?.LogEvent(2000, "tank temperature:", new Dictionary<string, object>()
        {
            { "temperature", $"{e.New.TemperatureHot!.Value.Celsius:N2}" }
        });

        displayController!.UpdateTemperature(e.New.TemperatureHot!.Value);

        displayController.UpdateStatus("Data sent!");
        Thread.Sleep(2000);
        displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));
    }

    public override Task Run()
    {
        Resolver.MeadowCloudService.SendLog(LogLevel.Information, "TankTemperatureMonitor started");

        return base.Run();
    }
}