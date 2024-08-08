using Meadow.Foundation.Grove.Relays;
using Meadow.Peripherals.Displays;
using YoshiPi;

namespace MeadowCloudCommands.Hardware;

internal class MeadowCloudCommandHardware : IMeadowCloudCommandHardware
{
    protected IYoshiPiHardware YoshiPi { get; }

    public IPixelDisplay Display { get; set; }

    public FourChannelSpdtRelay FourChannelRelay { get; set; }

    public MeadowCloudCommandHardware(IYoshiPiHardware yoshiPi)
    {
        YoshiPi = yoshiPi;
    }

    public void Initialize()
    {
        Display = YoshiPi.Display;

        FourChannelRelay = new FourChannelSpdtRelay(YoshiPi.GroveI2c, 0x11);
    }
}