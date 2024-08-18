using Meadow;
using Meadow.Hardware;
using System.Threading.Tasks;
using WifiWeather.Hardware;
using YoshiPi;

namespace WifiWeather;

public class MeadowApp : YoshiPiApp
{
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);

        var hardware = new WifiWeatherHardware(Hardware);
        var network = Hardware.ComputeModule.NetworkAdapters.Primary<INetworkAdapter>();

        mainController = new MainController(hardware, network);
        mainController.Initialize();

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        Resolver.Log.Info("Run...");

        await mainController?.Run();
    }
}