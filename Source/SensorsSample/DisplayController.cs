using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using Meadow.Peripherals.Displays;
using Meadow.Units;

namespace SensorsSample;

public class DisplayController
{
    private IColorInvertableDisplay display;
    private DisplayScreen displayScreen;

    private Label temperature;
    private Label humidity;

    public DisplayController(IColorInvertableDisplay display)
    {
        this.display = display;

        displayScreen = new DisplayScreen((IPixelDisplay)display, RotationType._270Degrees);

        displayScreen.BackgroundColor = Color.FromHex("FFFFFF");

        displayScreen.Controls.Add(new Box(
            left: 0,
            top: 0,
            width: displayScreen.Width,
            height: displayScreen.Height)
        {
            ForeColor = Color.FromHex("FFFFFF")
        });

        temperature = new Label(
            left: 0,
            top: 75,
            width: displayScreen.Width,
            height: 32)
        {
            Text = "TEMPERATURE:0.0°C",
            Font = new Font12x16(),
            TextColor = Color.FromHex("1E2834"),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        displayScreen.Controls.Add(temperature);

        humidity = new Label(
            left: 0,
            top: 135,
            width: displayScreen.Width,
            height: 32)
        {
            Text = "HUMIDITY:0.0%",
            Font = new Font12x16(),
            TextColor = Color.FromHex("1E2834"),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        displayScreen.Controls.Add(humidity);
    }

    public void UpdateTemperatureValue(Temperature temperatureValue)
    {
        temperature.Text = $"TEMPERATURE:{temperatureValue.Celsius:F1}°C";
    }

    public void UpdateHumidityValue(RelativeHumidity humidityValue)
    {
        humidity.Text = $"HUMIDITY:{humidityValue.Percent:F1}%";
    }
}