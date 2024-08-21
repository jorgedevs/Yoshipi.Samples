using GroveRelayController.Hardware;
using Meadow;
using System.Threading.Tasks;
using YoshiPi;

namespace GroveRelayController;

public class MeadowApp : YoshiPiApp
{
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);

        var hardware = new GroveRelayControllerHardware(Hardware);
        mainController = new MainController(hardware);

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        Resolver.Log.Info("Run...");

        await mainController.Start();

        //return Task.CompletedTask;
    }
}