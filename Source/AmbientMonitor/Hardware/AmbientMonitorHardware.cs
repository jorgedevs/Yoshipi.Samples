using Meadow.Foundation.Sensors;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors;
using Meadow.Peripherals.Sensors.Atmospheric;
using Meadow.Peripherals.Sensors.Buttons;
using Meadow.Units;
using YoshiPi;

namespace AmbientMonitor.Hardware;

public class AmbientMonitorHardware : IAmbientMonitorHardware
{
    private readonly IYoshiPiHardware yoshiPi;

    private readonly IButton? leftButton;
    private readonly IButton? rightButton;
    private readonly IColorInvertableDisplay? display;
    private readonly INetworkAdapter? networkAdapter;
    private readonly ITemperatureSensor temperatureSimulator;
    private readonly IBarometricPressureSensor barometricPressureSensor;
    private readonly IHumiditySensor humiditySensor;

    public IButton? LeftButton => leftButton;

    public IButton? RightButton => rightButton;

    public IColorInvertableDisplay? Display => display;

    public INetworkAdapter? NetworkAdapter => networkAdapter;

    public ITemperatureSensor? TemperatureSensor => temperatureSimulator;

    public IBarometricPressureSensor? BarometricPressureSensor => barometricPressureSensor;

    public IHumiditySensor? HumiditySensor => humiditySensor;

    public AmbientMonitorHardware(IYoshiPiHardware yoshiPi)
    {
        this.yoshiPi = yoshiPi;

        display = yoshiPi.Display;

        leftButton = yoshiPi.Button1;

        rightButton = yoshiPi.Button2;

        temperatureSimulator = new SimulatedTemperatureSensor(
            new Temperature(20, Temperature.UnitType.Celsius),
            new Temperature(18, Temperature.UnitType.Celsius),
            new Temperature(25, Temperature.UnitType.Celsius));

        barometricPressureSensor = new SimulatedBarometricPressureSensor();

        humiditySensor = new SimulatedHumiditySensor();

        networkAdapter = MeadowApp.Hardware.ComputeModule.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
    }
}