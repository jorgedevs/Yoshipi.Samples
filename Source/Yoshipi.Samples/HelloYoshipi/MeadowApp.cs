using Meadow;
using System.Threading.Tasks;
using YoshiPi;

namespace HelloYoshipi;

public class MeadowApp : YoshiPiApp
{
    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        var displayController = new DisplayController(Hardware.Display!);

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        Resolver.Log.Info("Run...");

        await Task.CompletedTask;
    }
}