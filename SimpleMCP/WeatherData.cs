using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleMCP;

/// <summary>
/// Structure for storing weather data
/// </summary>
public struct WeatherData
{

    public static WeatherData Empty {  get; set; } = new WeatherData();

    [JsonPropertyName("main")]
    public MainData Main { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherDescription> Weather { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    
    public struct MainData
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }


    public struct WeatherDescription
    {
        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}

