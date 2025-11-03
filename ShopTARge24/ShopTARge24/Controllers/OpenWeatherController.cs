using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ShopTARge24.ApplicationServices;
using ShopTARge24.ApplicationServices.Services;

namespace ShopTARge24.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly OpenWeatherService _openweatherService;
        public OpenWeatherController()
        {
            _openweatherService = new OpenWeatherService();
        }

        //Url päring
        [HttpGet("/OpenWeather")]
        //Hoiab mälus Tallinnat kuniks kasutaja linna otsib
        public async Task<IActionResult> Index(string city = "Tallinn")
        {
            var weatherData = await _openweatherService.GetWeatherAsync(city);
            ViewData["WeatherData"] = weatherData;
            ViewData["City"] = city;

            return View();
        }
    }
}
