using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using Meadow.Foundation.Hmi;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GroveRelayController.Controllers;

public class DisplayController
{
    public event EventHandler<bool> Relay0Toggled = default!;
    public event EventHandler<bool> Relay1Toggled = default!;
    public event EventHandler<bool> Relay2Toggled = default!;
    public event EventHandler<bool> Relay3Toggled = default!;

    private readonly int rowHeight = 60;
    private readonly int rowMargin = 15;

    private IColorInvertableDisplay display;
    private ICalibratableTouchscreen touchscreen;

    private Color backgroundColor = Color.FromHex("#F3F7FA");
    private Color foregroundColor = Color.White;

    private Font12x20 font12X20 = new Font12x20();
    private Font6x8 font6x8 = new Font6x8();

    private Image relayOn = Image.LoadFromResource("GroveRelayController.Resources.img_relay_on.bmp");
    private Image relayOff = Image.LoadFromResource("GroveRelayController.Resources.img_relay_off.bmp");

    private DisplayScreen displayScreen;

    private AbsoluteLayout splashLayout;
    private AbsoluteLayout dataLayout;

    private Button toggleRelay0;
    private Button toggleRelay1;
    private Button toggleRelay2;
    private Button toggleRelay3;

    private Label status;

    private bool isRelay0On;
    private bool isRelay1On;
    private bool isRelay2On;
    private bool isRelay3On;

    public DisplayController(IColorInvertableDisplay display, ICalibratableTouchscreen touchscreen)
    {
        this.display = display;
        this.touchscreen = touchscreen;

        displayScreen = new DisplayScreen((IPixelDisplay)display, RotationType._270Degrees, touchscreen)
        {
            BackgroundColor = backgroundColor
        };
    }

    private async Task CheckTouchscreenCalibration()
    {
        var calfile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ts.cal"));

        Resolver.Log.Info($"Using calibration data at {calfile.FullName}");

        var cal = new TouchscreenCalibrationService(displayScreen, calfile);
        //cal.EraseCalibrationData();

        var existing = cal.GetSavedCalibrationData();

        if (existing != null)
        {
            touchscreen.SetCalibrationData(existing);
        }
        else
        {
            await cal.Calibrate(true);
        }
    }

    private void CreateLayouts()
    {
        LoadSplashLayout();

        Thread.Sleep(3000);

        LoadDataLayout();
    }

    private void LoadSplashLayout()
    {
        splashLayout = new AbsoluteLayout(0, 0, displayScreen.Width, displayScreen.Height);

        var image = Image.LoadFromResource("GroveRelayController.Resources.img_meadow.bmp");
        var displayImage = new Picture(0, 0, displayScreen.Width, displayScreen.Height, image)
        {
            BackColor = Color.FromHex("#B35E2C"),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        splashLayout.Controls.Add(displayImage);

        displayScreen.Controls.Add(splashLayout);
    }

    private void LoadDataLayout()
    {
        dataLayout = new AbsoluteLayout(0, 0, displayScreen.Width, displayScreen.Height);

        dataLayout.Controls.Add(new Box(0, 0, displayScreen.Width, rowHeight)
        {
            ForeColor = Color.FromHex("844936")
        });

        status = new Label(rowMargin, 20, displayScreen.Width / 2, 20)
        {
            Text = "Grove Relay Controller",
            TextColor = foregroundColor,
            Font = font12X20,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        dataLayout.Controls.Add(status);

        dataLayout.Controls.Add(new Box(0, rowHeight, displayScreen.Width, displayScreen.Height - rowHeight)
        {
            ForeColor = Color.FromHex("B35E2C")
        });

        int relayWidth = 71;
        int relayHeight = 156;
        int margin = 12;
        int relaySpacing = 4;
        int smallMargin = 1;

        toggleRelay0 = new Button(
            margin,
            rowHeight + margin,
            relayWidth,
            relayHeight)
        {
            Image = relayOff,
            ForeColor = Color.FromHex("EE8343"),
            PressedColor = Color.FromHex("844936"),
            HighlightColor = Color.FromHex("EE8343"),
            ShadowColor = Color.FromHex("844936")
        };
        toggleRelay0.Clicked += ToggleRelay0_Clicked;
        dataLayout.Controls.Add(toggleRelay0);
        dataLayout.Controls.Add(new Label(
            margin,
            rowHeight + margin + smallMargin * 3,
            relayWidth,
            font6x8.Height + smallMargin * 2)
        {
            Text = $"RELAY 0",
            TextColor = foregroundColor,
            Font = font6x8,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        toggleRelay1 = new Button(
            margin + relayWidth + relaySpacing,
            rowHeight + margin,
            relayWidth,
            relayHeight)
        {
            Image = relayOff,
            ForeColor = Color.FromHex("EE8343"),
            PressedColor = Color.FromHex("844936"),
            HighlightColor = Color.FromHex("EE8343"),
            ShadowColor = Color.FromHex("844936")
        };
        toggleRelay1.Clicked += ToggleRelay1_Clicked;
        dataLayout.Controls.Add(toggleRelay1);
        dataLayout.Controls.Add(new Label(
            margin + relayWidth + relaySpacing,
            rowHeight + margin + smallMargin * 3,
            relayWidth,
            font6x8.Height + smallMargin * 2)
        {
            Text = $"RELAY 1",
            TextColor = foregroundColor,
            Font = font6x8,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        toggleRelay2 = new Button(
            margin + relayWidth * 2 + relaySpacing * 2,
            rowHeight + margin,
            relayWidth,
            relayHeight)
        {
            Image = relayOff,
            ForeColor = Color.FromHex("EE8343"),
            PressedColor = Color.FromHex("844936"),
            HighlightColor = Color.FromHex("EE8343"),
            ShadowColor = Color.FromHex("844936")
        };
        toggleRelay2.Clicked += ToggleRelay2_Clicked;
        dataLayout.Controls.Add(toggleRelay2);
        dataLayout.Controls.Add(new Label(
            margin + relayWidth * 2 + relaySpacing * 2,
            rowHeight + margin + smallMargin * 3,
            relayWidth,
            font6x8.Height + smallMargin * 2)
        {
            Text = $"RELAY 2",
            TextColor = foregroundColor,
            Font = font6x8,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        toggleRelay3 = new Button(
            margin + relayWidth * 3 + relaySpacing * 3,
            rowHeight + margin,
            relayWidth,
            relayHeight)
        {
            Image = relayOff,
            ForeColor = Color.FromHex("EE8343"),
            PressedColor = Color.FromHex("844936"),
            HighlightColor = Color.FromHex("EE8343"),
            ShadowColor = Color.FromHex("844936")
        };
        toggleRelay3.Clicked += ToggleRelay3_Clicked;
        dataLayout.Controls.Add(toggleRelay3);
        dataLayout.Controls.Add(new Label(
            margin + relayWidth * 3 + relaySpacing * 3,
            rowHeight + margin + smallMargin * 3,
            relayWidth,
            font6x8.Height + smallMargin * 2)
        {
            Text = $"RELAY 3",
            TextColor = foregroundColor,
            Font = font6x8,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        displayScreen.Controls.Add(dataLayout);
    }

    private void ToggleRelay0_Clicked(object? sender, EventArgs e)
    {
        isRelay0On = !isRelay0On;
        toggleRelay0.Image = isRelay0On ? relayOn : relayOff;
        Relay0Toggled?.Invoke(this, isRelay0On);
    }

    private void ToggleRelay1_Clicked(object? sender, EventArgs e)
    {
        isRelay1On = !isRelay1On;
        toggleRelay1.Image = isRelay1On ? relayOn : relayOff;
        Relay1Toggled?.Invoke(this, isRelay1On);
    }

    private void ToggleRelay2_Clicked(object? sender, EventArgs e)
    {
        isRelay2On = !isRelay2On;
        toggleRelay2.Image = isRelay2On ? relayOn : relayOff;
        Relay2Toggled?.Invoke(this, isRelay2On);
    }

    private void ToggleRelay3_Clicked(object? sender, EventArgs e)
    {
        isRelay3On = !isRelay3On;
        toggleRelay3.Image = isRelay3On ? relayOn : relayOff;
        Relay3Toggled?.Invoke(this, isRelay3On);
    }

    private void UpdateStatus(string status)
    {
        this.status.Text = status;
    }

    public async Task Run()
    {
        await CheckTouchscreenCalibration();
        CreateLayouts();
    }
}