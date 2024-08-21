using Meadow.Foundation.Grove.Relays;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using YoshiPi;

namespace GroveRelayController.Hardware;

public class GroveRelayControllerHardware : IGroveRelayControllerHardware
{
    private readonly IColorInvertableDisplay? display;
    private readonly ICalibratableTouchscreen? touchscreen;
    private readonly FourChannelSpdtRelay? fourChannelRelay;

    public IColorInvertableDisplay? Display => display;

    public ICalibratableTouchscreen? Touchscreen => touchscreen;

    public FourChannelSpdtRelay? FourChannelRelay => fourChannelRelay;

    public GroveRelayControllerHardware(IYoshiPiHardware yoshiPi)
    {
        display = yoshiPi.Display;

        touchscreen = yoshiPi.Touchscreen;

        fourChannelRelay = new FourChannelSpdtRelay(yoshiPi.GroveI2c, 0x11);
    }
}