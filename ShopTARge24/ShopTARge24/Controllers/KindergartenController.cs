using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Data;
using ShopTARge24.Models.Kindergarten;

namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly KindergartenContext _context;

        public KindergartenController
            (
                KindergartenContext context
            )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergarten.Select(x => new KindergartenIndexViewModel
            {
                Id = x.Id,
                GroupName = x.GroupName,
                ChildrenCount = x.ChildrenCount,
                KindergartenName = x.KindergartenName,
                TeacherName = x.TeacherName
            });

            return View(result);
        }
    }
}
