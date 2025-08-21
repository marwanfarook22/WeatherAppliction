using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppliction.WeatherSection
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public record Clouds(
 int all
    );

    public record Coord(
 double lon,
 double lat
    );

    public record Main(
 double temp,
 double feels_like,
 double temp_min,
 double temp_max,
 int pressure,
 int humidity,
 int sea_level,
 int grnd_level
    );

    public record Root(
 Coord coord,
 IReadOnlyList<Weather> weather,
 string @base,
 Main main,
 int visibility,
 Wind wind,
 Clouds clouds,
 int dt,
 Sys sys,
 int timezone,
 int id,
 string name,
 int cod
    );

    public record Sys(
 string country,
 int sunrise,
 int sunset
    );

    public record Weather(
 int id,
 string main,
 string description,
 string icon
    );

    public record Wind(
 double speed,
 int deg,
 double gust
    );



}
