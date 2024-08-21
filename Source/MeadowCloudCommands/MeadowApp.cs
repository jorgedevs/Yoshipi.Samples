using Meadow;
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

        Hardware.Display.InvertDisplayColor(true);

        var hardware = new MeadowCloudCommandHardware(Hardware);
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