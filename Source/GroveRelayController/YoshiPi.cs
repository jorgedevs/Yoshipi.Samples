using FieldStack;
using FieldStack.Drivers.Displays;
using FieldStack.Drivers.ICs.IOExpanders;
using FieldStack.Drivers.Relays;
using FieldStack.Drivers.Sensors.Buttons;
using FieldStack.Drivers.Sensors.Hid;
using FieldStack.Hardware;
using FieldStack.Peripherals.Displays;
using FieldStack.Peripherals.Relays;
using FieldStack.Peripherals.Sensors.Buttons;
using FieldStack.Units;

namespace FieldStack.Samples;

/// <summary>
/// The YoshiPi v1.b carrier board, as far as these samples need it.
///
/// Pin assignments come from the board's own SDK — Source/dotnet/Driver/YoshiPi_v1b.cs in
/// github.com/yoshimoshi-garage/yoshipi — rather than from reading the schematic, so they are
/// values that have run on the hardware. Peripherals other than the display are created on first
/// use, so a sample only claims the pins it needs.
/// </summary>
public sealed class YoshiPi : IDisposable
{
    private readonly RaspberryPi device;
    private readonly Mcp23008 expander;
    private readonly IDigitalOutputPort backlight;

    private PushButton? button1;
    private PushButton? button2;
    private Xpt2046? touchscreen;
    private Relay? relay1;
    private Relay? relay2;
    private II2cBus? qwiic;

    /// <summary>
    /// The onboard 240x320 ILI9341 TFT.
    /// </summary>
    public Ili9341 Display { get; }

    /// <summary>
    /// The user button on MCP23008 GP6.
    /// </summary>
    public IButton Button1 => button1 ??= new PushButton(
        expander.Pins.GP6.CreateDigitalInterruptPort(InterruptMode.EdgeBoth, ResistorMode.InternalPullUp));

    /// <summary>
    /// The user button on MCP23008 GP5.
    /// </summary>
    public IButton Button2 => button2 ??= new PushButton(
        expander.Pins.GP5.CreateDigitalInterruptPort(InterruptMode.EdgeBoth, ResistorMode.InternalPullUp));

    /// <summary>
    /// The XPT2046 resistive touchscreen over the display, on SPI bus 1.
    /// </summary>
    public Xpt2046 Touchscreen => touchscreen ??= new Xpt2046(
        device.CreateSpiBus(1, new Frequency(1, Frequency.UnitType.Megahertz)),
        device.Pins.GPIO26.CreateDigitalInterruptPort(InterruptMode.EdgeBoth, ResistorMode.Disabled),
        expander.Pins.GP7.CreateDigitalOutputPort(true),
        RotationType.Normal);

    /// <summary>
    /// The relay on GPIO16.
    /// </summary>
    public IRelay Relay1 => relay1 ??= new Relay(device.Pins.GPIO16.CreateDigitalOutputPort(false));

    /// <summary>
    /// The relay on MCP23008 GP3.
    /// </summary>
    public IRelay Relay2 => relay2 ??= new Relay(expander.Pins.GP3.CreateDigitalOutputPort(false));

    /// <summary>
    /// The Qwiic / Grove I2C bus (the Pi's default I2C bus).
    /// </summary>
    public II2cBus Qwiic => qwiic ??= device.CreateI2cBus();

    private YoshiPi(RaspberryPi device, Mcp23008 expander, Ili9341 display, IDigitalOutputPort backlight)
    {
        this.device = device;
        this.expander = expander;
        Display = display;
        this.backlight = backlight;
    }

    /// <summary>
    /// Brings up the display and its backlight.
    ///
    /// The panel on this board renders colour inverted with the driver's default of INVOFF, so
    /// display inversion is enabled here. That is a property of the glass fitted to the carrier,
    /// not of the ILI9341 controller.
    /// </summary>
    /// <param name="device">The host Raspberry Pi.</param>
    /// <param name="spiSpeed">
    /// Bus speed for the panel. The board SDK defaults to 10 MHz; its own drawing sample runs at
    /// 62.5 MHz.
    /// </param>
    public static YoshiPi Create(RaspberryPi device, Frequency spiSpeed, ColorMode colorMode, bool invertColor)
    {
        var expander = new Mcp23008(
            device.CreateI2cBus(1),
            address: (byte)Mcp23008.Addresses.Default,
            interruptPort: device.Pins.GPIO18.CreateDigitalInterruptPort(InterruptMode.EdgeRising, ResistorMode.Disabled),
            resetPort: device.Pins.GPIO17.CreateDigitalOutputPort(false));

        var display = new Ili9341(
            device.CreateSpiBus(0, spiSpeed),
            chipSelectPort: device.Pins.GPIO4.CreateDigitalOutputPort(true),
            dataCommandPort: device.Pins.GPIO23.CreateDigitalOutputPort(),
            resetPort: device.Pins.GPIO24.CreateDigitalOutputPort(),
            width: 240,
            height: 320,
            colorMode: colorMode);

        display.InvertDisplayColor(invertColor);

        var backlight = expander.Pins.GP4.CreateDigitalOutputPort(true);
        backlight.State = true;

        return new YoshiPi(device, expander, display, backlight);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        backlight.State = false;
        backlight.Dispose();
        Display.Dispose();
    }
}
