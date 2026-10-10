using FieldStack;
using FieldStack.Drivers.Sensors.Temperature;
using FieldStack.Peripherals.Displays;
using FieldStack.Samples;
using FieldStack.Units;

namespace TankTemperatureMonitor;

public class TankTemperatureMonitorApp : App<RaspberryPi>
{
    private YoshiPi hardware = default!;
    private DisplayController? displayController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        hardware = YoshiPi.Create(Device, new Frequency(62, Frequency.UnitType.Megahertz), ColorMode.Format16bppRgb565, invertColor: true);
        displayController = new DisplayController((IPixelDisplay)hardware.Display);

        var temperatureSensor = new Mcp9601(hardware.Qwiic);
        temperatureSensor.Updated += TemperatureSensorUpdated;
        temperatureSensor.StartUpdating(TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private void TemperatureSensorUpdated(object? sender, (Temperature? TemperatureHot, Temperature? TemperatureCold) e)
    {
        displayController!.UpdateStatus("Sending data...");
        Thread.Sleep(2000);

        // There is no cloud logging in this sample; the reading goes to the application log.
        Resolver.Log.Info($"tank temperature: {e.TemperatureHot!.Value.Celsius:N2}C");

        displayController.UpdateTemperature(e.TemperatureHot!.Value);

        displayController.UpdateStatus("Data sent!");
        Thread.Sleep(2000);
        displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));
    }

    public override Task Run()
    {
        Resolver.Log.Info("TankTemperatureMonitor started");

        return Task.CompletedTask;
    }
}
