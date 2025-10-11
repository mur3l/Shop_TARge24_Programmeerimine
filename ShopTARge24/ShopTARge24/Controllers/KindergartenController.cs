using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.Kindergarten;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly IKindergartenServices _kindergartenServices;
        private readonly IFileServices fileServices;

        public KindergartenController(
            ShopTARge24Context context,
            IKindergartenServices kindergartenServices,
            IFileServices fileServices)
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
            this.fileServices = fileServices;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateUpdate", vm);
            }

            var dto = new KindergartenDto
            {
                Id = Guid.NewGuid(),
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                Files = vm.Files
            };

            var created = await _kindergartenServices.Create(dto);

            await fileServices.FilesToApi(dto, created ?? new Kindergarten { Id = dto.Id });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dto.Id });
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
                    ImageId = y.Id,
                    KindergartenId = y.KindergartenId,
                    ImageTitle = y.ImageTitle,
                    ImageData = y.ImageData,
                    Image = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(y.ImageData))
                })
                .ToListAsync();

            var vm = new KindergartenCreateUpdateViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName,
                Images = images
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Images = await _context.FileToApis
                    .Where(x => x.KindergartenId == vm.Id)
                    .Select(y => new ImageViewModel
                    {
                        Filepath = y.ExistingFilePath,
                        ImageId = y.Id,
                        KindergartenId = y.KindergartenId,
                        ImageTitle = y.ImageTitle,
                        ImageData = y.ImageData,
                        Image = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(y.ImageData))
                    })
                    .ToListAsync();

                return View("CreateUpdate", vm);
            }

            var dto = new KindergartenDto
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                Files = vm.Files
            };

            var updated = await _kindergartenServices.Update(dto);

            await fileServices.FilesToApi(dto, updated ?? new Kindergarten { Id = dto.Id });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveImage(Guid imageId, Guid kindergartenId)
        {
            await fileServices.RemoveImageFromApi(new FileToApiDto { Id = imageId });

            return RedirectToAction("Update","Kindergarten", new { id = kindergartenId });
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
                    ImageId = y.Id,
                    KindergartenId = y.KindergartenId,
                    ImageTitle = y.ImageTitle,
                    ImageData = y.ImageData,
                    Image = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(y.ImageData))
                })
                .ToListAsync();

            var vm = new KindergartenDeleteViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName,
                Images = images
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                .ToListAsync();

            var vm = new KindergartenDetailsViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                TeacherName = kindergarten.TeacherName,
                Images = images
            };

            return View(vm);
        }
    }
}
