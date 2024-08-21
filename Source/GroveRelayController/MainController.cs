using GroveRelayController.Controllers;
using GroveRelayController.Hardware;
using Meadow.Peripherals.Relays;
using System.Threading.Tasks;

namespace GroveRelayController;

public class MainController
{
    private IGroveRelayControllerHardware hardware;
    private DisplayController displayController;

    public MainController(IGroveRelayControllerHardware hardware)
    {
        this.hardware = hardware;

        displayController = new DisplayController(hardware.Display, hardware.Touchscreen);

        displayController.Relay0Toggled += (s, e) =>
        {
            hardware.FourChannelRelay.Relays[0].State = e
                    ? RelayState.Closed
                    : RelayState.Open;
        };

        displayController.Relay1Toggled += (s, e) =>
        {
            hardware.FourChannelRelay.Relays[1].State = e
                    ? RelayState.Closed
                    : RelayState.Open;
        };

        displayController.Relay2Toggled += (s, e) =>
        {
            hardware.FourChannelRelay.Relays[2].State = e
                    ? RelayState.Closed
                    : RelayState.Open;
        };

        displayController.Relay3Toggled += (s, e) =>
        {
            hardware.FourChannelRelay.Relays[3].State = e
                    ? RelayState.Closed
                    : RelayState.Open;
        };
    }

    public async Task Start()
    {
        await displayController.Run();
    }
}