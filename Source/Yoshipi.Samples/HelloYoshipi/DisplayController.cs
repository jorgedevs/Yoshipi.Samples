using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using Meadow.Peripherals.Displays;

namespace HelloYoshipi;

public class DisplayController
{
    private readonly DisplayScreen displayScreen;

    private Label label;

    public DisplayController(IPixelDisplay display)
    {
        displayScreen = new DisplayScreen(display, RotationType._270Degrees)
        {
            BackgroundColor = Color.FromHex("14607F")
        };

        label = new Label(
            left: 0,
            top: 0,
            width: displayScreen.Width,
            height: displayScreen.Height)
        {
            Text = "Hello World",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Font = new Font12x20()
        };
        displayScreen.Controls.Add(label);
    }
}