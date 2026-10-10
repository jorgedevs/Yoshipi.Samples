using FieldStack;
using FieldStack.Drivers.Sensors;
using FieldStack.Peripherals.Displays;
using FieldStack.Samples;
using FieldStack.Units;

namespace SensorsSample;

public class SensorsSampleApp : App<RaspberryPi>
{
    private YoshiPi hardware = default!;
    private DisplayController? displayController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        hardware = YoshiPi.Create(Device, new Frequency(62, Frequency.UnitType.Megahertz), ColorMode.Format16bppRgb565, invertColor: true);
        displayController = new DisplayController(hardware.Display);

        // Real sensors (using an Htu21d sensor)
        //var sensor = new Htu21d(hardware.Qwiic);
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

    private void TemperatureSensorUpdated(object? sender, Temperature e)
    {
        displayController?.UpdateTemperatureValue(e);
    }

    private void HumiditySensorUpdated(object? sender, RelativeHumidity e)
    {
        displayController?.UpdateHumidityValue(e);
    }
}
