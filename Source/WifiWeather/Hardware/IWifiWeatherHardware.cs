using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors.Buttons;

namespace WifiWeather.Hardware;

public interface IWifiWeatherHardware
{
    IButton UpButton { get; }

    IButton DownButton { get; }

    IColorInvertableDisplay Display { get; }

    void Initialize();
}