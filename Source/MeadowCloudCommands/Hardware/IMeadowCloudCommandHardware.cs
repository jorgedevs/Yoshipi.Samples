using Meadow.Peripherals.Displays;

namespace MeadowCloudCommands.Hardware;

internal interface IMeadowCloudCommandHardware
{
    public IColorInvertableDisplay Display { get; }

    //public FourChannelSpdtRelay FourChannelRelay { get; }

    public void Initialize();
}