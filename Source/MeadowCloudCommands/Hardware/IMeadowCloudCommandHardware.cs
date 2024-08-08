using Meadow.Foundation.Grove.Relays;
using Meadow.Peripherals.Displays;

namespace MeadowCloudCommands.Hardware;

internal interface IMeadowCloudCommandHardware
{
    public IPixelDisplay Display { get; }

    public FourChannelSpdtRelay FourChannelRelay { get; }

    public void Initialize();
}