using FieldStack.Hardware;
using FieldStack.Peripherals.Displays;
using FieldStack.Peripherals.Sensors.Hid;
using FieldStack.Samples;

namespace GroveRelayController.Hardware;

public class GroveRelayControllerHardware : IGroveRelayControllerHardware
{
    private readonly IColorInvertableDisplay? display;
    private readonly ICalibratableTouchscreen? touchscreen;
    private readonly FourChannelSpdtRelay? fourChannelRelay;

    public IColorInvertableDisplay? Display => display;

    public ICalibratableTouchscreen? Touchscreen => touchscreen;

    public FourChannelSpdtRelay? FourChannelRelay => fourChannelRelay;

    public GroveRelayControllerHardware(YoshiPi yoshiPi)
    {
        display = yoshiPi.Display;

        touchscreen = yoshiPi.Touchscreen;

        fourChannelRelay = new FourChannelSpdtRelay(yoshiPi.Qwiic, 0x11);
    }
}
