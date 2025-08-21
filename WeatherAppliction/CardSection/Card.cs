using WeatherAppliction.ForcastingDataClass;

namespace WeatherAppliction.CardSection;

public interface ISimpleForecastCard
{
    void CreateCard(List forecastItem);
}

public class SimpleForecastCard : Panel, ISimpleForecastCard
{
    private Label timeLabel;
    private Label iconLabel;
    private Label tempLabel;
    private Label conditionLabel;
    private Label detailsLabel;

    public SimpleForecastCard()
    {
        InitializeCard();
    }

    private void InitializeCard()
    {
        // Card properties
        Size = new Size(200, 280);
        BackColor = Color.White;
        Margin = new Padding(8, 20, 8, 8);
        BorderStyle = BorderStyle.None;
        Cursor = Cursors.Hand;

        // Add card styling
        Paint += OnCardPaint;

        CreateCardElements();
    }

    private void OnCardPaint(object sender, PaintEventArgs e)
    {
        var rect = ClientRectangle;
        var graphics = e.Graphics;
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Draw shadow
        using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
        {
            graphics.FillRectangle(shadowBrush, new Rectangle(4, 4, rect.Width - 4, rect.Height - 4));
        }

        // Draw card background
        using (var backgroundBrush = new SolidBrush(Color.White))
        {
            graphics.FillRectangle(backgroundBrush, new Rectangle(0, 0, rect.Width - 4, rect.Height - 4));
        }

        // Draw border
        using (var borderPen = new Pen(Color.FromArgb(230, 230, 230), 1))
        {
            graphics.DrawRectangle(borderPen, new Rectangle(0, 0, rect.Width - 5, rect.Height - 5));
        }
    }

    private void CreateCardElements()
    {
        // Time/Date Label
        timeLabel = new Label
        {
            Text = "Loading...",
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = Color.FromArgb(51, 65, 85),
            Location = new Point(15, 15),
            Size = new Size(170, 40),
            TextAlign = ContentAlignment.TopLeft
        };

        // Weather Icon
        iconLabel = new Label
        {
            Text = "🌤",
            Font = new Font("Segoe UI", 36F),
            ForeColor = Color.FromArgb(251, 146, 60),
            Location = new Point(15, 60),
            Size = new Size(70, 60),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Temperature
        tempLabel = new Label
        {
            Text = "--°C",
            Font = new Font("Segoe UI", 24F, FontStyle.Bold),
            ForeColor = Color.FromArgb(15, 23, 42),
            Location = new Point(90, 70),
            Size = new Size(100, 40),
            TextAlign = ContentAlignment.MiddleLeft
        };

        // Weather Condition
        conditionLabel = new Label
        {
            Text = "Loading condition...",
            Font = new Font("Segoe UI", 11F),
            ForeColor = Color.FromArgb(71, 85, 105),
            Location = new Point(15, 140),
            Size = new Size(170, 30),
            TextAlign = ContentAlignment.TopLeft
        };

        // Additional Details
        detailsLabel = new Label
        {
            Text = "Details loading...",
            Font = new Font("Segoe UI", 9F),
            ForeColor = Color.FromArgb(100, 116, 139),
            Location = new Point(15, 180),
            Size = new Size(170, 80),
            TextAlign = ContentAlignment.TopLeft
        };

        // Add all controls to the card
        Controls.AddRange(new Control[]
        {
                timeLabel, iconLabel, tempLabel, conditionLabel, detailsLabel
        });
    }

    // Public method to update card with forecast data
    public void CreateCard(List forecastItem)
    {
        // Update time/date
        var dateTime = DateTimeOffset.FromUnixTimeSeconds(forecastItem.dt).DateTime;
        var dayName = dateTime.Date == DateTime.Today ? "Today" :
                     dateTime.Date == DateTime.Today.AddDays(1) ? "Tomorrow" :
                     dateTime.ToString("ddd");
        var timeStr = dateTime.ToString("HH:mm");
        var dateStr = dateTime.ToString("MMM dd");

        timeLabel.Text = $"{dayName}\n{dateStr} {timeStr}";

        // Update weather icon
        var weather = forecastItem.weather.FirstOrDefault();
        if (weather != null)
        {
            iconLabel.Text = GetWeatherIcon(weather.icon);
            iconLabel.ForeColor = GetWeatherIconColor(weather.icon);
            conditionLabel.Text = CapitalizeWords(weather.description);
        }

        // Update temperature
        tempLabel.Text = $"{ConvertKelvinToCelsius(forecastItem.main.temp):F0}°C";

        // Update details
        var details = new List<string>
            {
                $"Feels like {ConvertKelvinToCelsius(forecastItem.main.feels_like):F0}°C",
                $"💧 Humidity: {forecastItem.main.humidity}%",
                $"💨 Wind: {forecastItem.wind.speed:F1} m/s"
            };

        if (forecastItem.pop > 0)
            details.Add($"🌧 Rain: {forecastItem.pop * 100:F0}%");

        if (forecastItem.rain != null && forecastItem.rain._3h > 0)
            details.Add($"☔ {forecastItem.rain._3h:F1}mm/3h");

        detailsLabel.Text = string.Join("\n", details);
    }

    // Helper methods
    private double ConvertKelvinToCelsius(double kelvin)
    {
        return kelvin - 273.15;
    }

    private string CapitalizeWords(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return string.Join(" ", input.Split(' ')
            .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }

    private string GetWeatherIcon(string iconCode)
    {
        return iconCode switch
        {
            "01d" or "01n" => "☀",   // clear sky
            "02d" or "02n" => "🌤",   // few clouds
            "03d" or "03n" => "⛅",   // scattered clouds
            "04d" or "04n" => "☁",   // broken clouds
            "09d" or "09n" => "🌦",   // shower rain
            "10d" or "10n" => "🌧",   // rain
            "11d" or "11n" => "⛈",   // thunderstorm
            "13d" or "13n" => "❄",   // snow
            "50d" or "50n" => "🌫",   // mist
            _ => "🌤"
        };
    }

    private Color GetWeatherIconColor(string iconCode)
    {
        return iconCode switch
        {
            "01d" or "01n" => Color.FromArgb(251, 146, 60),    // Orange
            "02d" or "02n" => Color.FromArgb(251, 146, 60),    // Orange
            "03d" or "03n" => Color.FromArgb(107, 114, 128),   // Gray
            "04d" or "04n" => Color.FromArgb(75, 85, 99),      // Dark Gray
            "09d" or "09n" => Color.FromArgb(59, 130, 246),    // Blue
            "10d" or "10n" => Color.FromArgb(37, 99, 235),     // Darker Blue
            "11d" or "11n" => Color.FromArgb(147, 51, 234),    // Purple
            "13d" or "13n" => Color.FromArgb(147, 197, 253),   // Light Blue
            "50d" or "50n" => Color.FromArgb(156, 163, 175),   // Light Gray
            _ => Color.FromArgb(251, 146, 60)
        };
    }
}