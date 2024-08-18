using Meadow.Foundation.Sensors;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Leds;
using Meadow.Peripherals.Sensors;
using Meadow.Peripherals.Sensors.Atmospheric;
using YoshiPi;

namespace MeadowCloudLogging.Hardware;

internal class MeadowCloudLoggingHardware : IMeadowCloudLoggingHardware
{
    protected IYoshiPiHardware yoshiPi { get; }

    public IColorInvertableDisplay Display { get; set; }

    public ITemperatureSensor TemperatureSensor { get; set; }

    public IBarometricPressureSensor BarometricPressureSensor { get; set; }

    public IHumiditySensor HumiditySensor { get; set; }

    public IRgbPwmLed RgbPwmLed { get; set; }

    public MeadowCloudLoggingHardware(IYoshiPiHardware projLab)
    {
        yoshiPi = projLab;
    }

    public void Initialize()
    {
        Display = yoshiPi.Display;

        TemperatureSensor = new SimulatedTemperatureSensor(
            new Meadow.Units.Temperature(22),
            new Meadow.Units.Temperature(20),
            new Meadow.Units.Temperature(26));

        BarometricPressureSensor = new SimulatedBarometricPressureSensor();

        HumiditySensor = new SimulatedHumiditySensor();
    }
}