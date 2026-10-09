using FieldStack.Hardware;
using FieldStack.Peripherals.Displays;
using FieldStack.Peripherals.Sensors;
using FieldStack.Peripherals.Sensors.Atmospheric;
using FieldStack.Peripherals.Sensors.Buttons;

namespace AzureIoTHubLogging.Hardware;

public interface IAzureIoTHubLoggingHardware
{
    IButton? LeftButton { get; }

    IButton? RightButton { get; }

    IColorInvertableDisplay? Display { get; }

    INetworkAdapter? NetworkAdapter { get; }

    ITemperatureSensor? TemperatureSensor { get; }

    IBarometricPressureSensor? BarometricPressureSensor { get; }

    IHumiditySensor? HumiditySensor { get; }
}