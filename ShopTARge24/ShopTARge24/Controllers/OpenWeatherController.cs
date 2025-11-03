using Microsoft.AspNetCore.Mvc;

namespace ShopTARge24.Controllers
{
    public class OpenWeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
