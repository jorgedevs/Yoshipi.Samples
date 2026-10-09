using FieldStack.Hardware;
using FieldStack.Samples;
using FieldStack.Peripherals.Displays;
using FieldStack.Peripherals.Sensors.Buttons;

namespace WifiWeather.Hardware;

public class WifiWeatherHardware : IWifiWeatherHardware
{
    private readonly IButton? leftButton;
    private readonly IButton? rightButton;
    private readonly IColorInvertableDisplay? display;
    private readonly INetworkAdapter? networkAdapter;

    public IButton? LeftButton => leftButton;

    public IButton? RightButton => rightButton;

    public IColorInvertableDisplay? Display => display;

    public INetworkAdapter? NetworkAdapter => networkAdapter;

    public WifiWeatherHardware(YoshiPi yoshiPi, INetworkAdapter? networkAdapter)
    {
        display = yoshiPi.Display;

        leftButton = yoshiPi.Button1;

        rightButton = yoshiPi.Button2;

        this.networkAdapter = networkAdapter;
    }
}