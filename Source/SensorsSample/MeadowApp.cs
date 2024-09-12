using Meadow;
using Meadow.Foundation.Sensors;
using Meadow.Units;
using System;
using System.Threading.Tasks;
using YoshiPi;

namespace SensorsSample;

public class MeadowApp : YoshiPiApp
{
    private DisplayController? displayController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);
        displayController = new DisplayController(Hardware.Display);

        var temperatureSensor = new SimulatedTemperatureSensor(
            initialTemperature: new Temperature(22.5),
            minimumTemperature: new Temperature(20.0),
            maximumTemperature: new Temperature(25.0));
        temperatureSensor.Updated += TemperatureSensorUpdated;
        temperatureSensor.StartUpdating(TimeSpan.FromSeconds(5));

        var humiditySensor = new SimulatedHumiditySensor();
        humiditySensor.Updated += HumiditySensorUpdated; ;
        humiditySensor.StartUpdating(TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void TemperatureSensorUpdated(object? sender, IChangeResult<Temperature> e)
    {
        displayController?.UpdateTemperatureValue(e.New);
    }

    private void HumiditySensorUpdated(object? sender, IChangeResult<RelativeHumidity> e)
    {
        displayController?.UpdateHumidityValue(e.New);
    }
}