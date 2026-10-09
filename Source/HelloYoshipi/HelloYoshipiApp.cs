using FieldStack;
using FieldStack.Peripherals.Displays;
using FieldStack.Samples;
using FieldStack.Units;

namespace HelloYoshipi;

public class HelloYoshipiApp : App<RaspberryPi>
{
    private YoshiPi hardware = default!;
    private DisplayController? displayController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        hardware = YoshiPi.Create(Device, new Frequency(62, Frequency.UnitType.Megahertz), ColorMode.Format16bppRgb565, invertColor: true);

        displayController = new DisplayController(hardware.Display, hardware.Touchscreen);

        return Task.CompletedTask;
    }

    public override async Task Run()
    {
        Resolver.Log.Info("Run...");

        await displayController!.Start();
    }
}
