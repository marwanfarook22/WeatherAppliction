
using WeatherAppliction.CitiesOptionsSection;
using WeatherAppliction.TablePanalSection;
using WeatherAppliction.WeatherSection;
using static WeatherAppliction.Program;

namespace WeatherAppliction;

public partial class MainForm : Form
{
    private readonly Cities cities = new Cities();
    private LoadingControl loadingControl = new LoadingControl();
    private IWeatherApi _weatherApi;
    private ITablePanalSection _tablePanal;


    public MainForm(IWeatherApi weatherApi, ITablePanalSection tablePanal)
    {
        _weatherApi = weatherApi;
        _tablePanal = tablePanal;

        InitializeComponent();
        CityNameBoxHandeling();
        this.Controls.Add(loadingControl);
    }

    private void CityNameBoxHandeling()
    {
        LoadCityNames();
        CityNameBox.Text = " loading... ";
    }

    public async void LoadCityNames()
    {
        await Task.Delay(3000); // Simulate a delay for loading city names
        CityNameBox.Text = "";
        // Fetch the city names from the CSV file
        List<CitysDetails> CitiesResult = cities.FetchCsvData();
        CityNameBox.Items.Clear();
        foreach (var city in CitiesResult)
        {
            CityNameBox.Items.Add(city.City);
        }


    }

    private void CityNameBox_TextChanged(object sender, EventArgs e)
    {
        ComboBox cb = sender as ComboBox;
        if (cb == null) return;
        List<CitysDetails> CollictionCsvData = cities.FetchCsvData();
        // Filter the cities based on the text in the ComboBox
        var FilteringCitysNames =
            CollictionCsvData.Where(city => city.City.StartsWith(cb.Text, StringComparison.OrdinalIgnoreCase))
            .Select(city => city.City)
            .ToList();


        cb.Items.Clear();
        cb.Items.AddRange(FilteringCitysNames.ToArray());
        cb.DroppedDown = true; // Automatically show the dropdown


    }

    private void addCityButton_Click(object sender, EventArgs e)
    {
        if (CityNameBox.Text == null || string.IsNullOrWhiteSpace(CityNameBox.Text))
        {
            MessageBox.Show("Please select a city from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        else
        {
            LoadWeatherDataAsync(CityNameBox.Text);
            loadingControl.ShowLoading("Please wait...");


        }
    }


    public async void LoadWeatherDataAsync(string cityName)
    {

        var DataResponed = await _weatherApi.FetchWeatherData(cityName);
        loadingControl.HideLoading();
        try
        {

            TableLayoutPanel weatherPanel = _tablePanal.GeneratePanel(DataResponed);
            _tablePanal.ArrangeAndAddWeatherPanel(weatherPanel, this);


        }
        catch (Exception ex)
        {

            MessageBox.Show(ex.Message, "Data UnLoaded", MessageBoxButtons.OK);

        }


    }
}
