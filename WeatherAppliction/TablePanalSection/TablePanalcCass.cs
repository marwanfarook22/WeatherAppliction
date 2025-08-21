using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppliction.WeatherSection;

namespace WeatherAppliction.TablePanalSection;

public interface ITablePanalSection
{
    void ArrangeAndAddWeatherPanel(TableLayoutPanel weatherPanel, Form parentForm);
    TableLayoutPanel GeneratePanel(Root weatherData);
}

public class TablePanalcCass : ITablePanalSection
{


    public void ArrangeAndAddWeatherPanel(TableLayoutPanel weatherPanel, Form parentForm)
    {
        // استخدام الفورم بدلاً من groupBox1
        int panelCount = parentForm.Controls.OfType<TableLayoutPanel>().Count();
        int panelSpacing = 10;
        int panelWidth = weatherPanel.Width;
        int panelHeight = weatherPanel.Height;

        // الحصول على العرض المتاح في الفورم
        int availableWidth = parentForm.ClientSize.Width;

        // حساب عدد الـ panels في كل صف
        int panelsPerRow = Math.Max(1, (availableWidth + panelSpacing) / (panelWidth + panelSpacing));

        // حساب الصف والعمود للـ panel الجديد
        int row = panelCount / panelsPerRow;
        int col = panelCount % panelsPerRow;

        int x = col * (panelWidth + panelSpacing);
        int y = 20 + row * (panelHeight + panelSpacing); // 20 هامش علوي

        weatherPanel.Location = new Point(x, y);

        // إضافة الـ panel إلى الفورم مباشرة
        parentForm.Controls.Add(weatherPanel);
    }


    public TableLayoutPanel GeneratePanel(Root weatherData)
    {
        try
        {

            string cityName = weatherData.name?.ToString();
            string country = weatherData.sys.country.ToString();

            string weatherCondition = weatherData.weather[0].description?.ToString();
            weatherCondition = char.ToUpper(weatherCondition[0]) + weatherCondition.Substring(1); // Capitalize first letter

            // Convert temperature from Kelvin to Celsius
            double tempKelvin = weatherData.main.temp;
            double tempCelsius = tempKelvin - 273.15;
            double feelsLikeKelvin = weatherData.main.feels_like;
            double feelsLikeCelsius = feelsLikeKelvin - 273.15;

            int humidity = weatherData.main.humidity;
            int pressure = weatherData.main.pressure;
            double windSpeed = weatherData.wind.speed;
            int windDeg = weatherData.wind.deg;
            int visibility = weatherData.visibility;

            // Convert visibility from meters to kilometers
            double visibilityKm = visibility / 1000.0;

            // Convert wind speed from m/s to km/h
            double windKmh = windSpeed * 3.6;

            // Get wind direction
            string windDirection = new WeatherApi().GetWindDirection(windDeg);

            // Convert sunrise/sunset from Unix timestamp
            long sunriseUnix = weatherData.sys.sunrise;
            long sunsetUnix = weatherData.sys.sunset;
            int timezone = weatherData.timezone;

            DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds(sunriseUnix).AddSeconds(timezone).DateTime;
            DateTime sunset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnix).AddSeconds(timezone).DateTime;

            // Get weather icon code for later use
            string iconCode = weatherData.weather[0].main.ToString().ToLower();

            // Create and configure tableLayoutPanel1
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Padding = new Padding(0, 50, 0, 0);
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Location = new Point(6, 26);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(409, 464);
            tableLayoutPanel1.TabIndex = 4;

            // Weather Condition
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(215, 49),
                Name = "WeatherCondition",
                Size = new Size(129, 20),
                TabIndex = 2,
                Text = weatherCondition,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            }, 1, 0);

            // City Name
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                ImageAlign = ContentAlignment.BottomCenter,
                Location = new Point(36, 168),
                Name = "CityName",
                Size = new Size(78, 20),
                TabIndex = 3,
                Text = $"{cityName}, {country}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            }, 0, 1);

            // Temperature
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(241, 168),
                Name = "Temperature",
                Size = new Size(77, 20),
                TabIndex = 4,
                Text = $"{tempCelsius:F1}°C",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Red
            }, 1, 1);

            // Feels Like
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(22, 261),
                Name = "FeelsLike",
                Size = new Size(107, 20),
                TabIndex = 5,
                Text = $"Feels like: {feelsLikeCelsius:F1}°C"
            }, 0, 2);

            // Humidity
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(212, 261),
                Name = "Humidity",
                Size = new Size(135, 20),
                TabIndex = 6,
                Text = $"💧 Humidity: {humidity}%"
            }, 1, 2);

            // Wind
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(4, 326),
                Name = "Wind",
                Size = new Size(142, 20),
                TabIndex = 7,
                Text = $"🌬️ Wind: {windKmh:F1} km/h {windDirection}"
            }, 0, 3);

            // Visibility
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(213, 326),
                Name = "Visibility",
                Size = new Size(133, 20),
                TabIndex = 8,
                Text = $"👁️ Visibility: {visibilityKm:F1}km"
            }, 1, 3);

            // Pressure
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(3, 376),
                Name = "Pressure",
                Size = new Size(145, 20),
                TabIndex = 9,
                Text = $"🌡️ Pressure: {pressure} hPa"
            }, 0, 4);

            // Sunrise
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(218, 376),
                Name = "Sunrise",
                Size = new Size(123, 20),
                TabIndex = 10,
                Text = $"🌅 Sunrise: {sunrise:HH:mm}"
            }, 1, 4);

            // UV Index (not available in this API, showing placeholder)
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(32, 413),
                Name = "CloudCover",
                Size = new Size(87, 20),
                TabIndex = 11,
                Text = $"☁️ Clouds: {weatherData.clouds.all}%"
            }, 0, 5);

            // Sunset
            tableLayoutPanel1.Controls.Add(new Label()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Location = new Point(218, 413),
                Name = "Sunset",
                Size = new Size(123, 20),
                TabIndex = 12,
                Text = $"🌇 Sunset: {sunset:HH:mm}"
            }, 1, 5);

            // Weather Icon (PictureBox)
            PictureBox WeatherImage = new PictureBox()
            {
                Anchor = AnchorStyles.None,
                BackColor = Color.Transparent,
                ErrorImage = null,
                Location = new Point(22, 22),
                Name = "pictureBox1",
                Size = new Size(107, 82),
                SizeMode = PictureBoxSizeMode.Zoom,
                TabIndex = 1,
                TabStop = false,
            };



            // Load weather Image from OpenWeatherMap
            try
            {
                WeatherImage.Image = new WeatherApi().GetWeatherImage(weatherData.weather[0].main);
            }
            catch (Exception ex)
            {
                WeatherImage.Image = new WeatherApi().LoadDefaultImage();
            }
            // Forecast Button
            Button forecastButton = new Button()
            {
                Anchor = AnchorStyles.None,
                Location = new Point(150, 471),
                Name = "ForecastButton",
                Size = new Size(120, 35),
                TabIndex = 13,
                Text = "📊 View Forecast",
                UseVisualStyleBackColor = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.LightBlue,
                ForeColor = Color.DarkBlue,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Add click event handler for the button
            forecastButton.Click += (sender, e) =>
            {
                // Navigate to forecast page
                // You can implement your navigation logic here
                // For example:
                var forecastForm = new ForcastingForm(weatherData.coord);
                forecastForm.Show();

                // or
                //NavigateToForecastPage(cityName);

            };

            // Add button spanning both columns in the last row
            tableLayoutPanel1.SetColumnSpan(forecastButton, 2);
            tableLayoutPanel1.Controls.Add(forecastButton, 0, 7);


            tableLayoutPanel1.Controls.Add(WeatherImage, 0, 0);

            return tableLayoutPanel1;
        }
        catch (NullReferenceException ex)
        {
            throw new Exception(ex.Message);

        }

    }

}
