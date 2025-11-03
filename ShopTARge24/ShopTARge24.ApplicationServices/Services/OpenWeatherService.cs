using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.ApplicationServices.Services
{
    public class OpenWeatherService
    {
        private readonly string apiKey = "707f2b54aafd3db7e43408772976a616";
        private readonly string baseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<string> GetWeatherAsync (string city)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"{baseUrl}?q={city}&appid={apiKey}&units=Metric";
                var response = await httpClient.GetStringAsync(url);

                JObject json = JObject.Parse(response);
                string cityName = json["name"]?.ToString();
                double temp = json["main"]?["temp"]?.ToObject<double>() ?? 0;
                string weather = json["weather"]?[0]?["description"]?.ToString();

                return $"City: {cityName} | Temperature: {temp:F1}°C | Weather: {weather}";
            }
        }
    }
}
