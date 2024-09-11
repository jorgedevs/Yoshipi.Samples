using AzureIoTHubLogging.Hardware;
using Meadow;
using System.Threading.Tasks;
using YoshiPi;

namespace AzureIoTHubLogging;

public class MeadowApp : YoshiPiApp
{
    private MainController? mainController;

    public override async Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);

        var hardware = new AzureIoTHubLoggingHardware(Hardware);
        mainController = new MainController(hardware);
        await mainController.Initialize();
    }

    public override Task Run()
    {
        Resolver.Log.Info("Run...");

        mainController?.Run();

        return Task.CompletedTask;
    }
}