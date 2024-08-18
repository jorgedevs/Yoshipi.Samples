using AmbientMonitor.Hardware;
using Meadow;
using System.Threading.Tasks;
using YoshiPi;

namespace AmbientMonitor;

public class MeadowApp : YoshiPiApp
{
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);

        var hardware = new AmbientMonitorHardware(Hardware);
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