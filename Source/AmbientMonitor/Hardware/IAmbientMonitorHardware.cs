using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors;
using Meadow.Peripherals.Sensors.Atmospheric;
using Meadow.Peripherals.Sensors.Buttons;

namespace AmbientMonitor.Hardware;

public interface IAmbientMonitorHardware
{
    IButton? LeftButton { get; }

    IButton? RightButton { get; }

    IColorInvertableDisplay? Display { get; }

    INetworkAdapter? NetworkAdapter { get; }

    ITemperatureSensor? TemperatureSensor { get; }

    IBarometricPressureSensor? BarometricPressureSensor { get; }

    IHumiditySensor? HumiditySensor { get; }
}