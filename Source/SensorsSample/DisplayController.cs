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

        displayScreen.BackgroundColor = Color.FromHex("056BBF");

        temperature = new Label(
            left: 0,
            top: 75,
            width: displayScreen.Width,
            height: 32)
        {
            Text = "TEMPERATURE:0.0°C",
            Font = new Font12x16(),
            TextColor = Color.White,
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
            TextColor = Color.White,
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