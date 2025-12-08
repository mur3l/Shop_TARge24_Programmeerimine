using Microsoft.AspNetCore.Mvc;

namespace ShopTARge24.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]


        public IActionResult Index()
        {
            return View();
        }
    }
}
