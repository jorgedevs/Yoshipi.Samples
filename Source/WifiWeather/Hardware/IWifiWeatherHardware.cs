using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Peripherals.Sensors.Buttons;

namespace WifiWeather.Hardware;

public interface IWifiWeatherHardware
{
    IButton? LeftButton { get; }

    IButton? RightButton { get; }

    IColorInvertableDisplay? Display { get; }

    INetworkAdapter? NetworkAdapter { get; }
}