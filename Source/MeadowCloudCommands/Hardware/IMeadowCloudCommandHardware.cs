using Meadow.Foundation.Grove.Relays;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;

namespace MeadowCloudCommands.Hardware;

public interface IMeadowCloudCommandHardware
{
    IColorInvertableDisplay? Display { get; }

    FourChannelSpdtRelay? FourChannelRelay { get; }

    INetworkAdapter? NetworkAdapter { get; }
}