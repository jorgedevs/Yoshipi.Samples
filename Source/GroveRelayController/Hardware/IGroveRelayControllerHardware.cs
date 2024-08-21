using Meadow.Foundation.Grove.Relays;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;

namespace GroveRelayController.Hardware;

public interface IGroveRelayControllerHardware
{
    IColorInvertableDisplay? Display { get; }

    ICalibratableTouchscreen? Touchscreen { get; }

    FourChannelSpdtRelay? FourChannelRelay { get; }
}