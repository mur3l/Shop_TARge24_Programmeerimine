using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class SpaceshipServices : ISpaceshipServices
    {
        private readonly ShopTARge24Context _context;

        //Construtor
        public SpaceshipServices
            (
                ShopTARge24Context context
            )
        {
             _context = context;
        }

        public async Task<Spaceships> Create(SpaceshipDto dto)
        {
            Spaceships spaceships = new Spaceships();

            spaceships.Id = Guid.NewGuid();
            spaceships.Name = dto.Name;
            spaceships.Classification = dto.Classification;
            spaceships.BuiltDate = dto.BuiltDate;
            spaceships.Crew = dto.Crew;
            spaceships.EnginePower = dto.EnginePower;
            spaceships.CreatedAt = DateTime.Now;
            spaceships.ModifiedAt = DateTime.Now;

            await _context.Spaceships.AddAsync(spaceships);
            await _context.SaveChangesAsync();

            return spaceships;
        }

        public async Task<Spaceships> Update(SpaceshipDto dto)
        {
            Spaceships spaceships = new Spaceships();

            spaceships.Id = dto.Id;
            spaceships.Name = dto.Name;
            spaceships.Classification = dto.Classification;
            spaceships.BuiltDate = dto.BuiltDate;
            spaceships.Crew = dto.Crew;
            spaceships.EnginePower = dto.EnginePower;
            spaceships.CreatedAt = dto.ModifiedAt;
            spaceships.ModifiedAt = DateTime.Now;

            _context.Spaceships.Update(spaceships);
            await _context.SaveChangesAsync();

            return spaceships;
        }

        public async Task<Spaceships> DetailAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        public async Task<Spaceships> Delete(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Spaceships.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}