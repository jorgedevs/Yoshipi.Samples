using Meadow.Foundation.Sensors;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors;
using Meadow.Peripherals.Sensors.Atmospheric;
using Meadow.Units;
using YoshiPi;

namespace MeadowCloudLogging.Hardware;

public class MeadowCloudLoggingHardware : IMeadowCloudLoggingHardware
{
    private readonly IColorInvertableDisplay? display;
    private readonly INetworkAdapter? networkAdapter;
    private readonly ITemperatureSensor? temperatureSensor;
    private readonly IBarometricPressureSensor? barometricPressureSensor;
    private readonly IHumiditySensor? humiditySensor;

    public IColorInvertableDisplay? Display => display;

    public ITemperatureSensor? TemperatureSensor => temperatureSensor;

    public IBarometricPressureSensor? BarometricPressureSensor => barometricPressureSensor;

    public IHumiditySensor? HumiditySensor => humiditySensor;

    public INetworkAdapter? NetworkAdapter => networkAdapter;

    public MeadowCloudLoggingHardware(IYoshiPiHardware yoshiPi)
    {
        display = yoshiPi.Display;

        temperatureSensor = new SimulatedTemperatureSensor(
            new Temperature(20, Temperature.UnitType.Celsius),
            new Temperature(18, Temperature.UnitType.Celsius),
            new Temperature(25, Temperature.UnitType.Celsius));

        barometricPressureSensor = new SimulatedBarometricPressureSensor();

        humiditySensor = new SimulatedHumiditySensor();

        networkAdapter = MeadowApp.Hardware.ComputeModule.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
    }
}