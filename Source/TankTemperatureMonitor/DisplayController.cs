using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using Meadow.Peripherals.Displays;
using Meadow.Units;
using System.Collections.Generic;

namespace TankTemperatureMonitor;

public class DisplayController
{
    private const int TEMPERATURE_READINGS = 10;

    private readonly int rowHeight = 60;
    private readonly int graphHeight = 115;
    private readonly int axisLabelsHeight = 15;
    private readonly int margin = 15;

    private Color TextColor = Color.White;
    private Color backgroundColor = Color.FromHex("575E3C");
    private Color foregroundColor = Color.FromHex("323626");
    private Color chartCurveColor = Color.FromHex("EF7D3B");

    private Font6x8 font6x8 = new Font6x8();
    private Font8x12 font8x12 = new Font8x12();
    private Font12x20 font12X20 = new Font12x20();

    private List<Temperature> temperatureLogs;
    private LineChartSeries lineChartSeries;
    private LineChart lineChart;

    private Label status;
    private Label latestReading;
    private Label axisLabels;
    private Label connectionErrorLabel;
    private Label temperatureLabel;

    private readonly DisplayScreen dataLayout;

    public DisplayController(IPixelDisplay display)
    {
        temperatureLogs = new List<Temperature>();

        dataLayout = new DisplayScreen(display, RotationType._270Degrees)
        {
            BackgroundColor = backgroundColor
        };

        dataLayout.Controls.Add(new Box(0, 0, dataLayout.Width, rowHeight)
        {
            ForeColor = foregroundColor
        });

        status = new Label(margin, 15, dataLayout.Width / 2, 20)
        {
            Text = "--:-- -- --/--/--",
            TextColor = TextColor,
            Font = font12X20,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        dataLayout.Controls.Add(status);

        latestReading = new Label(margin, 37, dataLayout.Width / 2, 8)
        {
            Text = "Latest Reading: --:-- -- --/--/--",
            TextColor = TextColor,
            Font = font6x8,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        dataLayout.Controls.Add(latestReading);

        dataLayout.Controls.Add(new Box(
            margin,
            rowHeight + margin,
            dataLayout.Width - margin * 2,
            graphHeight + axisLabelsHeight + 20)
        {
            ForeColor = foregroundColor
        });

        lineChart = new LineChart(
            margin,
            rowHeight + margin,
            dataLayout.Width - margin * 2,
            graphHeight)
        {
            BackgroundColor = foregroundColor,
            AxisColor = TextColor,
            ShowYAxisLabels = true,
            AlwaysShowYOrigin = false,
        };
        lineChartSeries = new LineChartSeries()
        {
            LineColor = chartCurveColor,
            PointColor = chartCurveColor,
            LineStroke = 1,
            PointSize = 2,
            ShowLines = true,
            ShowPoints = true,
        };
        lineChart.Series.Add(lineChartSeries);
        dataLayout.Controls.Add(lineChart);

        axisLabels = new Label(
            margin,
            margin + rowHeight + graphHeight,
            dataLayout.Width - margin * 2,
            axisLabelsHeight)
        {
            Text = "Y: Celcius | X: Every hour",
            TextColor = TextColor,
            BackColor = foregroundColor,
            Font = font6x8,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        dataLayout.Controls.Add(axisLabels);

        temperatureLabel = new Label(
            left: 0,
            top: 196,
            width: dataLayout.Width,
            height: 32)
        {
            Text = "Temperature:0.0°C",
            Font = new Font12x16(),
            TextColor = TextColor,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        dataLayout.Controls.Add(temperatureLabel);
    }

    public void UpdateTemperature(Temperature temperature)
    {
        temperatureLabel.Text = $"Temperature: {temperature.Celsius:F1}°C";

        if (temperatureLogs.Count > TEMPERATURE_READINGS)
        {
            temperatureLogs.RemoveAt(0);
        }

        temperatureLogs.Add(temperature);

        lineChartSeries.Points.Clear();
        for (int i = 0; i < temperatureLogs.Count; i++)
        {
            lineChartSeries.Points.Add(i, temperatureLogs[i].Celsius);
        }
    }

    public void UpdateStatus(string status)
    {
        this.status.Text = status;
    }
}