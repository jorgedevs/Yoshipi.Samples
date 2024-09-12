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

        // Real sensors (using an Htu21d sensor)
        //var sensor = new Htu21d(Hardware.Qwiic);
        //var temperatureSensor = (ITemperatureSensor)sensor;
        //var humiditySensor = (IHumiditySensor)sensor;

        // Simulated sensors
        var temperatureSensor = new SimulatedTemperatureSensor(
            initialTemperature: new Temperature(22.5),
            minimumTemperature: new Temperature(20.0),
            maximumTemperature: new Temperature(25.0));
        var humiditySensor = new SimulatedHumiditySensor();

        temperatureSensor.Updated += TemperatureSensorUpdated;
        temperatureSensor.StartUpdating(TimeSpan.FromSeconds(1));

        humiditySensor.Updated += HumiditySensorUpdated;
        humiditySensor.StartUpdating(TimeSpan.FromSeconds(1));

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