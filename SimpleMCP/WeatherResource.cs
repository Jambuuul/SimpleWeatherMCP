using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMCP
{
    [McpServerResourceType]
    public class WeatherResource
    {


        /// <summary>
        /// Example of data stored on a server
        /// </summary>
        /// <returns>example</returns>
        [McpServerResource(UriTemplate = "weather://testResourse", Name = "Direct Text Resource")]
        [Description("Direct Test Resource")]
        public static string DirectTextResource() => "Weather is good!";

        /// <summary>
        /// Gives info about the weather as a resource
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns>weather info</returns>
        [McpServerResource(UriTemplate = "weather://{cityName}", Name = "Weather Resource")]
        [Description("Get weather info based on city name but resource")]
        public static async Task<string> GetWeather(string cityName)
        {

            var resp = (await WeatherManager.GetWeatherAsync(cityName));

            if (!resp.Success)
            {
                return resp.Error!.Message;
            }
            var dict = resp.Data;


            // manually adding weather data to the response
            StringBuilder sb = new();
            foreach (KeyValuePair<string, object> kvp in dict)
            {
                sb.Append($"{kvp.Key} : {kvp.Value},\n");
            }
            return sb.ToString();
        }


    }
}
