using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Data;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.Kindergarten;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly IKindergartenServices _kindergartenServices;
        private readonly IFileServices _fileServices;

        public KindergartenController(ShopTARge24Context context, IKindergartenServices kindergartenServices)
            
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
            _fileServices = _fileServices;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergarten
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var result = new KindergartenCreateUpdateViewModel();
            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto
            {
                Id = Guid.NewGuid(),
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                Files = vm.Files
            };
            
            var result = await _kindergartenServices.Create(dto);

            _fileServices.FilesToApi(dto, result);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);
            if (kindergarten == null) return NotFound();

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == kindergarten.Id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id
                })
                .ToArrayAsync();

            var vm = new KindergartenCreateUpdateViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName
            };

            await _kindergartenServices.Update(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);
            if (kindergarten == null) return NotFound();

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id
                })
                .ToArrayAsync();

            var vm = new KindergartenDeleteViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            await _kindergartenServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);
            if (kindergarten == null) return NotFound();

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == kindergarten.Id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id,
                    KindergartenId = y.KindergartenId,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(y.ImageData))
                })
                .ToArrayAsync();

            var vm = new KindergartenDetailsViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName
            };

            return View(vm);
        }
    }
}
