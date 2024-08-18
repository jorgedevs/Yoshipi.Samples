using Meadow;
using MeadowCloudLogging.Hardware;
using System.Threading.Tasks;
using YoshiPi;

namespace MeadowCloudLogging;

public class MeadowApp : YoshiPiApp
{
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);

        var hardware = new MeadowCloudLoggingHardware(Hardware);
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