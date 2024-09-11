using Meadow.Foundation.Sensors;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors;
using Meadow.Peripherals.Sensors.Atmospheric;
using Meadow.Peripherals.Sensors.Buttons;
using Meadow.Units;
using YoshiPi;

namespace AzureIoTHubLogging.Hardware;

public class AzureIoTHubLoggingHardware : IAzureIoTHubLoggingHardware
{
    private readonly IButton? leftButton;
    private readonly IButton? rightButton;
    private readonly IColorInvertableDisplay? display;
    private readonly INetworkAdapter? networkAdapter;
    private readonly ITemperatureSensor? temperatureSensor;
    private readonly IBarometricPressureSensor? barometricPressureSensor;
    private readonly IHumiditySensor? humiditySensor;

    public IButton? LeftButton => leftButton;

    public IButton? RightButton => rightButton;

    public IColorInvertableDisplay? Display => display;

    public INetworkAdapter? NetworkAdapter => networkAdapter;

    public ITemperatureSensor? TemperatureSensor => temperatureSensor;

    public IBarometricPressureSensor? BarometricPressureSensor => barometricPressureSensor;

    public IHumiditySensor? HumiditySensor => humiditySensor;

    public AzureIoTHubLoggingHardware(IYoshiPiHardware yoshiPi)
    {
        display = yoshiPi.Display;

        leftButton = yoshiPi.Button1;

        rightButton = yoshiPi.Button2;

        temperatureSensor = new SimulatedTemperatureSensor(
            new Temperature(20, Temperature.UnitType.Celsius),
            new Temperature(18, Temperature.UnitType.Celsius),
            new Temperature(25, Temperature.UnitType.Celsius));

        humiditySensor = new SimulatedHumiditySensor();

        barometricPressureSensor = new SimulatedBarometricPressureSensor();

        networkAdapter = MeadowApp.Hardware.ComputeModule.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
    }
}