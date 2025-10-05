using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileServices _fileServices;

        public RealEstateServices(ShopTARge24Context context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            var realEstate = new RealEstate
            {
                Id = Guid.NewGuid(),
                Area = dto.Area,
                Location = dto.Location,
                RoomNumber = dto.RoomNumber,
                BuildingType = dto.BuildingType,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _fileServices.FilesToApi(dto, realEstate);

            await _context.RealEstates.AddAsync(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }

        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            var realEstate = await _context.RealEstates.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (realEstate == null) return null;

            realEstate.Area = dto.Area;
            realEstate.Location = dto.Location;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.ModifiedAt = DateTime.Now;

            _fileServices.FilesToApi(dto, realEstate);

            _context.RealEstates.Update(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }

        public async Task<RealEstate> DetailAsync(Guid id)
        {
            return await _context.RealEstates.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RealEstate> Delete(Guid id)
        {
            var result = await _context.RealEstates.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return null;

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    RealEstateId = y.RealEstateId,
                    ExistingFilePath = y.ExistingFilePath
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromApi(images);

            _context.RealEstates.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
