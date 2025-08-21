using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherAppliction.ForcastingDataClass;
using WeatherAppliction.WeatherSection;
using static WeatherAppliction.Program;

namespace WeatherAppliction;

public partial class ForcastingForm : Form
{
    private LoadingControl loadingControl = new LoadingControl();

    

    public ForcastingForm(WeatherSection.Coord coord)
    {
        this.Controls.Add(loadingControl);
        InitializeComponent();
        ForcastingWeatherService(coord);
        loadingControl.ShowLoading("Please wait...");
    }


    async void ForcastingWeatherService(WeatherSection.Coord _coord)
    {
        const string ApiKey = "62cf580d1c7b6bf8cf887710efd517b2";
        HttpClient client = new HttpClient();
        try
        {
            HttpResponseMessage response = await client.GetAsync
                ($"https://api.openweathermap.org/data/2.5/forecast?lat={_coord.lat}&lon={_coord.lon}&appid={ApiKey}");
            response.EnsureSuccessStatusCode();
            string DataRsponed = await response.Content.ReadAsStringAsync();

            loadingControl.HideLoading();

            IReadOnlyList<List> Weather_Forcasting_Data = JsonSerializer.Deserialize<ForcastingRoot>(DataRsponed)!.list;
            LoadData(Weather_Forcasting_Data);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Somthing Went Wrong in {ex.Message}", "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }

    
}
