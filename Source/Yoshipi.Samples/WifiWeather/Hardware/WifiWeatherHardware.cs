using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors.Buttons;
using YoshiPi;

namespace WifiWeather.Hardware;

public class WifiWeatherHardware : IWifiWeatherHardware
{
    protected IYoshiPiHardware ProjLab { get; private set; }

    public IButton UpButton { get; set; }

    public IButton DownButton { get; set; }

    public IPixelDisplay Display { get; set; }

    public WifiWeatherHardware(IYoshiPiHardware projLab)
    {
        ProjLab = projLab;
    }

    public void Initialize()
    {
        UpButton = ProjLab.Button1;

        DownButton = ProjLab.Button2;

        Display = ProjLab.Display;
    }
}