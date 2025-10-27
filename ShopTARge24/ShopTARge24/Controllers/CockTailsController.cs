using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Models.CockTails;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopTARge24.Controllers
{
    public class CockTailsController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<IActionResult> Index(string searchTerm = "margarita")
        {
            string apiUrl = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={searchTerm}";

            string json = await _httpClient.GetStringAsync(apiUrl);

            var root = JsonSerializer.Deserialize<Root>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var viewModel = new CockTailsViewModel
            {
                drinks = root?.drinks ?? new List<CockTailsModel>()
            };

            ViewBag.SearchTerm = searchTerm;

            return View(viewModel);
        }
    }
}
