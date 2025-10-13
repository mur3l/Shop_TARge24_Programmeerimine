using System.Text.Json;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.Dto.Serviceinterface;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            var response = $"https://api.weatherapi.com/v1/vurrent.json";

            using (var client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(response);
                string json = await httpResponse.Content.ReadAsStringAsync();

                List<AccuLocationRootDto> weatherData =
                    JsonSerializer.Deserialize<List<AccuLocationRootDto>>(json);

                //Tallinn 127964 AccuWeather Kood
            }

            return dto;
        }
    }
}
