using Meadow;
using Meadow.Hardware;
using MeadowCloudCommands.Controllers;
using MeadowCloudCommands.Hardware;
using System.Threading.Tasks;
using YoshiPi;

namespace MeadowCloudCommands;

public class MeadowApp : YoshiPiApp
{
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        var hardware = new MeadowCloudCommandHardware(Hardware);
        var network = Hardware.ComputeModule.NetworkAdapters.Primary<IWiFiNetworkAdapter>();

        mainController = new MainController(hardware, network);
        mainController.Initialize();

        return Task.CompletedTask;
    }

    public override Task Run()
    {
        Resolver.Log.Info("Run...");

        mainController?.Run();

        return Task.CompletedTask;
    }
}