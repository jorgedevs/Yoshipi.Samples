using Meadow.Foundation.Grove.Relays;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using YoshiPi;

namespace MeadowCloudCommands.Hardware;

public class MeadowCloudCommandHardware : IMeadowCloudCommandHardware
{
    private readonly IColorInvertableDisplay? display;
    private readonly FourChannelSpdtRelay? fourChannelRelay;
    private readonly INetworkAdapter? networkAdapter;

    public IColorInvertableDisplay? Display => display;

    public FourChannelSpdtRelay? FourChannelRelay => fourChannelRelay;

    public INetworkAdapter? NetworkAdapter => networkAdapter;

    public MeadowCloudCommandHardware(IYoshiPiHardware yoshiPi)
    {
        display = yoshiPi.Display;

        fourChannelRelay = new FourChannelSpdtRelay(yoshiPi.GroveI2c, 0x11);

        networkAdapter = MeadowApp.Hardware.ComputeModule.NetworkAdapters.Primary<IWiFiNetworkAdapter>();
    }
}