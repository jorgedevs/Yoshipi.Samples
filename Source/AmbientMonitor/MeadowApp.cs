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

        var hardware = new AmbientMonitorHardware(Hardware);
        mainController = new MainController();
        mainController.Initialize(hardware);

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        Resolver.Log.Info("Run...");

        await mainController?.Run();
    }
}