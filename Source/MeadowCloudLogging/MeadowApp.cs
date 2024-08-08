using Meadow;
using Meadow.Hardware;
using MeadowCloudLogging.Controllers;
using MeadowCloudLogging.Hardware;
using System.Threading.Tasks;
using YoshiPi;

namespace MeadowCloudLogging;

public class MeadowApp : YoshiPiApp
{
    private MainController mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        var hardware = new MeadowCloudLoggingHardware(Hardware);
        var network = Hardware.ComputeModule.NetworkAdapters.Primary<IWiFiNetworkAdapter>();

        mainController = new MainController(hardware, network);
        mainController.Initialize();

        return Task.CompletedTask;
    }

    public override Task Run()
    {
        Resolver.Log.Info("Run...");

        mainController.Run();

        return Task.CompletedTask;
    }
}