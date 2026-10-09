using FieldStack.Hardware;
using FieldStack.Peripherals.Displays;
using FieldStack.Peripherals.Sensors.Buttons;

namespace WifiWeather.Hardware;

public interface IWifiWeatherHardware
{
    IButton? LeftButton { get; }

    IButton? RightButton { get; }

    IColorInvertableDisplay? Display { get; }

    INetworkAdapter? NetworkAdapter { get; }
}