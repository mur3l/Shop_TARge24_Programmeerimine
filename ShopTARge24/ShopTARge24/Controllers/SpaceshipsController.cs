using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly ISpaceshipServices _spaceshipServices;
        private readonly IFileServices _fileServices;

        public SpaceshipsController(
            ShopTARge24Context context,
            ISpaceshipServices spaceshipServices,
            IFileServices fileServices)
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
            _fileServices = fileServices;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateUpdate", new SpaceshipCreateUpdateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files
            };

            var created = await _spaceshipServices.Create(dto);

            if (vm.Files != null && vm.Files.Count > 0)
            {
                _fileServices.FilesToApi(dto, created ?? new Spaceships { Id = dto.Id });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = created?.Id ?? dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailAsync(id);
            if (spaceship == null) return NotFound();

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id,
                    SpaceshipId = y.SpaceshipId,
                    ImageTitle = y.ImageTitle,
                    ImageData = y.ImageData,
                    Image = y.ImageData != null
                        ? $"data:image/jpg;base64,{Convert.ToBase64String(y.ImageData)}"
                        : null
                })
                .ToArrayAsync();

            var vm = new SpaceshipCreateUpdateViewModel
            {
                Id = spaceship.Id,
                Name = spaceship.Name,
                Classification = spaceship.Classification,
                BuiltDate = spaceship.BuiltDate,
                Crew = spaceship.Crew,
                EnginePower = spaceship.EnginePower,
                CreatedAt = spaceship.CreatedAt,
                ModifiedAt = spaceship.ModifiedAt
            };
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files
            };

            var updated = await _spaceshipServices.Update(dto);

            if (vm.Files != null && vm.Files.Count > 0)
            {
                _fileServices.FilesToApi(dto, updated ?? new Spaceships { Id = dto.Id });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailAsync(id);
            if (spaceship == null) return NotFound();

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id,
                    SpaceshipId = y.SpaceshipId,
                    ImageTitle = y.ImageTitle,
                    ImageData = y.ImageData,
                    Image = y.ImageData != null
                        ? $"data:image/jpg;base64,{Convert.ToBase64String(y.ImageData)}"
                        : null
                })
                .ToArrayAsync();

            var vm = new SpaceshipDeleteViewModel
            {
                Id = spaceship.Id,
                Name = spaceship.Name,
                Classification = spaceship.Classification,
                BuiltDate = spaceship.BuiltDate,
                Crew = spaceship.Crew,
                EnginePower = spaceship.EnginePower,
                CreatedAt = spaceship.CreatedAt,
                ModifiedAt = spaceship.ModifiedAt
            };
            vm.ImageViewModels.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceship = await _spaceshipServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailAsync(id);
            if (spaceship == null) return NotFound();

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id,
                    SpaceshipId = y.SpaceshipId,
                    ImageTitle = y.ImageTitle,
                    ImageData = y.ImageData,
                    Image = y.ImageData != null
                        ? $"data:image/jpg;base64,{Convert.ToBase64String(y.ImageData)}"
                        : null
                })
                .ToArrayAsync();

            var vm = new SpaceshipDetailsViewModel
            {
                Id = spaceship.Id,
                Name = spaceship.Name,
                Classification = spaceship.Classification,
                BuiltDate = spaceship.BuiltDate,
                Crew = spaceship.Crew,
                EnginePower = spaceship.EnginePower,
                CreatedAt = spaceship.CreatedAt,
                ModifiedAt = spaceship.ModifiedAt
            };
            vm.Images.AddRange(images);

            return View(vm);
        }
    }
}
