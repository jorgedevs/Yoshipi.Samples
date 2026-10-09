using AzureIoTHubLogging.Hardware;
using FieldStack;
using FieldStack.Hardware;
using FieldStack.Peripherals.Displays;
using FieldStack.Samples;
using FieldStack.Units;

namespace AzureIoTHubLogging;

public class AzureIoTHubLoggingApp : App<RaspberryPi>
{
    private YoshiPi yoshiPi = default!;
    private MainController? mainController;

    public override async Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        yoshiPi = YoshiPi.Create(Device, new Frequency(62, Frequency.UnitType.Megahertz), ColorMode.Format16bppRgb565, invertColor: true);

        var hardware = new AzureIoTHubLoggingHardware(yoshiPi, Device.NetworkAdapters.Primary<IWiFiNetworkAdapter>());
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
