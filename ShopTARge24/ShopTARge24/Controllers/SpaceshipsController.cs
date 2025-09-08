using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Data;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge24Context _context;

        public SpaceshipsController
            (
                ShopTARge24Context context
            )
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Classification = x.Classification,
                    BuiltDate = x.BuiltDate,
                    Crew = x.Crew,
                });

            return View(result);
        }
    }
}
