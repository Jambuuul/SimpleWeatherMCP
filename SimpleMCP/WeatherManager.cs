using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleMCP;

[McpServerToolType]
public static class WeatherManager
{

    private readonly static string _apiKey = "94e6e26f2a7da7aa738469cd6838c23c";
    private readonly static HttpClient _httpClient = new();

    /// <summary>
    /// Gets weather info based on city name
    /// </summary>
    /// <param name="cityName"></param>
    /// <returns>weather info</returns>
    [McpServerTool(Name = "get_weather"), Description("Gets weather information based on city name")]
    public static async Task<JsonResponse> GetWeatherAsync(string cityName)
    {
        try
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}&units=metric&lang=en";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            _ = response.EnsureSuccessStatusCode(); // Выбрасывает исключение при ошибке HTTP

            string json = await response.Content.ReadAsStringAsync();

           
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(json, options);
            JsonResponse output = new()
            {
                Success = true,
                Data = WeatherDataToDict(weatherData),
                Error = null
            };

            return output;
        }
        catch (HttpRequestException)
        {
            return Failure(503, "Unknown city");
        }
        catch (JsonException)
        {
            return Failure(500, "Error parsing API response");
        }
        catch (Exception)
        {
            return Failure(404, "Unknown error");
        }
    }


    /// <summary>
    /// Returns dictionary with needed data from WeatherData object.
    /// </summary>
    /// <param name="data">WeatherData object</param>
    /// <returns>needed info</returns>
    private static Dictionary<string, object> WeatherDataToDict(WeatherData data)
    {
        // actually, WeatherData object contains more info than we need
        // but we use not much info for showcase purposes
        Dictionary<string, object> dict = new()
        {
            ["name"] = data.Name,
            ["condition"] = data.Weather.FirstOrDefault().Description ?? "no info",
            ["temperature"] = data.Main.Temp,
            ["humidity"] = data.Main.Humidity
        };

        return dict;
    }

    /// <summary>
    /// Forms a failure response
    /// </summary>
    /// <param name="code">error code</param>
    /// <param name="msg">error message</param>
    /// <returns></returns>
    private static JsonResponse Failure(int code, string msg) =>
        new JsonResponse
        {
            Success = false,
            Data = null,
            Error = new ErrorInfo(code, msg)
        };
}
