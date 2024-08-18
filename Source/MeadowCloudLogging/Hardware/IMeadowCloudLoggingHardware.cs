using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors;
using Meadow.Peripherals.Sensors.Atmospheric;

namespace MeadowCloudLogging.Hardware;

public interface IMeadowCloudLoggingHardware
{
    IColorInvertableDisplay? Display { get; }

    ITemperatureSensor? TemperatureSensor { get; }

    IBarometricPressureSensor? BarometricPressureSensor { get; }

    IHumiditySensor? HumiditySensor { get; }

    INetworkAdapter? NetworkAdapter { get; }
}