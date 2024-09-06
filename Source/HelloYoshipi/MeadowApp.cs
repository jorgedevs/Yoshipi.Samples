using Meadow;
using Meadow.Foundation.Sensors.Atmospheric;
using System;
using System.Threading.Tasks;
using YoshiPi;

namespace HelloYoshipi;

public class MeadowApp : YoshiPiApp
{
    Htu21d? sensor;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initializing...");

        sensor = new Htu21d(Hardware.GroveI2c);

        sensor.Updated += (sender, result) =>
        {
            Resolver.Log.Info($"  Temperature: {result.New.Temperature?.Celsius:F1}C");
            Resolver.Log.Info($"  Relative Humidity: {result.New.Humidity?.Percent:F1}%");
        };

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        if (sensor == null) { return; }
        sensor.StartUpdating(TimeSpan.FromSeconds(1));
    }
}