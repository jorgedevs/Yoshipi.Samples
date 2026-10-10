using FieldStack.Hardware;
using FieldStack.Peripherals.Displays;
using FieldStack.Peripherals.Sensors.Hid;

namespace GroveRelayController.Hardware;

public interface IGroveRelayControllerHardware
{
    IColorInvertableDisplay Display { get; }

    ICalibratableTouchscreen Touchscreen { get; }

    FourChannelSpdtRelay FourChannelRelay { get; }
}
