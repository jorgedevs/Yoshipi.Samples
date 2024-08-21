using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors.Buttons;
using YoshiPi;

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

    public WifiWeatherHardware(IYoshiPiHardware yoshiPi)
    {
        display = yoshiPi.Display;

        leftButton = yoshiPi.Button1;

        rightButton = yoshiPi.Button2;

        networkAdapter = MeadowApp.Hardware.ComputeModule.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
    }
}