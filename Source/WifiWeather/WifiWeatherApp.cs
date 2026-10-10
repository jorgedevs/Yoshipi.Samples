using FieldStack;
using FieldStack.Hardware;
using FieldStack.Peripherals.Displays;
using FieldStack.Samples;
using FieldStack.Units;
using WifiWeather.Hardware;

namespace WifiWeather;

public class WifiWeatherApp : App<RaspberryPi>
{
    private YoshiPi yoshiPi = default!;
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        yoshiPi = YoshiPi.Create(Device, new Frequency(62, Frequency.UnitType.Megahertz), ColorMode.Format16bppRgb565, invertColor: true);

        var hardware = new WifiWeatherHardware(yoshiPi, Device.NetworkAdapters.Primary<IWiFiNetworkAdapter>());
        mainController = new MainController(hardware);

        return Task.CompletedTask;
    }

    public override Task Run()
    {
        Resolver.Log.Info("Run...");

        mainController?.Run();

        return Task.CompletedTask;
    }
}
