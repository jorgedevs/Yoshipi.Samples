using FieldStack.Drivers;
using FieldStack.Hardware;
using FieldStack.Peripherals.Relays;

namespace GroveRelayController.Hardware;

/// <summary>
/// The Seeed Grove 4-channel SPDT relay. An STM32 on the board takes a single channel bitmask
/// written to register 0x10 over I2C. FieldStack has no driver for this board, so the sample
/// carries this minimal one (ported from Meadow.Foundation.Grove).
/// </summary>
public class FourChannelSpdtRelay
{
    private const byte ChannelControl = 0x10;

    private readonly I2cCommunications i2c;
    private byte channelStates;

    /// <summary>
    /// The four relays, indexed 0 to 3.
    /// </summary>
    public IRelay[] Relays { get; }

    public FourChannelSpdtRelay(II2cBus i2cBus, byte address = 0x11)
    {
        i2c = new I2cCommunications(i2cBus, address);

        Relays = new IRelay[4];
        for (var i = 0; i < Relays.Length; i++)
        {
            Relays[i] = new Channel(this, (byte)(1 << i));
        }

        SetAllOff();
    }

    public void SetAllOff() => Write(0x00);

    public void SetAllOn() => Write(0x0F);

    private void Write(byte states)
    {
        channelStates = states;
        i2c.WriteRegister(ChannelControl, channelStates);
    }

    private void SetChannel(byte mask, bool on) => Write(on ? (byte)(channelStates | mask) : (byte)(channelStates & ~mask));

    private sealed class Channel : IRelay
    {
        private readonly FourChannelSpdtRelay owner;
        private readonly byte mask;

        public Channel(FourChannelSpdtRelay owner, byte mask)
        {
            this.owner = owner;
            this.mask = mask;
        }

        public event EventHandler<RelayState>? OnChanged;

        public RelayType Type => RelayType.NormallyOpen;

        public RelayState State
        {
            get => (owner.channelStates & mask) != 0 ? RelayState.Closed : RelayState.Open;
            set
            {
                owner.SetChannel(mask, value == RelayState.Closed);
                OnChanged?.Invoke(this, value);
            }
        }

        public void Toggle() => State = State == RelayState.Closed ? RelayState.Open : RelayState.Closed;
    }
}
