using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppliction.CitiesOptionsSection;
public class Cities
{


    public List<CitysDetails> CitiesColliction { get; set; }


    public List<CitysDetails> FetchCsvData()
    {

        using (var reader = new StreamReader("1000Cities.csv") { })
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            CitiesColliction = csv.GetRecords<CitysDetails>().ToList();
            return CitiesColliction;

        }


    }



}




public class CitysDetails
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }


    public string City { get; set; }

    public string Country { get; set; }

    public override string ToString()
    {
        return $"{City}, {Country} ({Latitude:F5}, {Longitude:F5})";
    }
}
