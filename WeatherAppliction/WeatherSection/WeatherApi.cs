using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WeatherAppliction.WeatherSection
{
    public interface IWeatherApi
    {
        Task<Root> FetchWeatherData(string location);
        string GetWindDirection(int degree);
        public Image LoadDefaultImage();
        public Image GetWeatherImage(string mainWeatherCondition);
      
    }

    public class WeatherApi : IWeatherApi
    {

        public async Task<Root> FetchWeatherData(string location)
        {
            const string ApiKey = "62cf580d1c7b6bf8cf887710efd517b2";
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={ApiKey}");
                responseMessage.EnsureSuccessStatusCode();
                string DataRsponed = await responseMessage.Content.ReadAsStringAsync();
                var roots = JsonSerializer.Deserialize<Root>(DataRsponed)!;
                return roots;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Somthing Went Wrong in {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null!;
            }
            finally
            {
                client.Dispose();
            }


        }

        public string GetWindDirection(int degree)
        {
            string[] directions = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE",
                          "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            int index = (int)Math.Round(degree / 22.5) % 16;
            return directions[index];
        }
        public Image GetWeatherImage(string mainWeatherCondition)
        {
            string imagePath = $"WeatherMain/{mainWeatherCondition}.png";

            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"Weather image not found: {imagePath}");
            }

            return Image.FromFile(imagePath);
        }
        public Image LoadDefaultImage()
        {
            string defaultPath = "WeatherMain/identification.png";

            if (File.Exists(defaultPath))
            {
                return Image.FromFile(defaultPath);
            }

            return CreatePlaceholderImage();
        }

        private Image CreatePlaceholderImage()
        {
            var bitmap = new Bitmap(64, 64);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.LightGray, 0, 0, 64, 64);
                graphics.DrawString("?", new Font("Arial", 20), Brushes.Black, 20, 20);
            }
            return bitmap;
        }

    
    }



}
