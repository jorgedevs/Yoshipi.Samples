using FieldStack;
using FieldStack.Peripherals.Displays;
using FieldStack.Samples;
using FieldStack.Units;
using GroveRelayController.Hardware;

namespace GroveRelayController;

public class GroveRelayControllerApp : App<RaspberryPi>
{
    private YoshiPi yoshiPi = default!;
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        yoshiPi = YoshiPi.Create(Device, new Frequency(62, Frequency.UnitType.Megahertz), ColorMode.Format16bppRgb565, invertColor: true);

        var hardware = new GroveRelayControllerHardware(yoshiPi);
        mainController = new MainController(hardware);

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        Resolver.Log.Info("Run...");

        await mainController!.Start();
    }
}
