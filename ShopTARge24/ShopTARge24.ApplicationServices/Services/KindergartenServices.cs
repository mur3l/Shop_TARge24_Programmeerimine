using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly KindergartenContext _context;

        public KindergartenServices
            (
                KindergartenContext context
            )
        {
            _context = context;
        }
        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new Kindergarten();
            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.TeacherName = dto.TeacherName;
            await _context.Kindergarten.AddAsync(kindergarten);
            await _context.SaveChangesAsync();
            return kindergarten;
        }

        public Task<Spaceships> Create(SpaceshipDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Spaceships> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var result = await _context.Kindergarten.FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public Task<Spaceships> Update(SpaceshipDto dto)
        {
            throw new NotImplementedException();
        }

        Task<Spaceships> IKindergartenServices.DetailAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
