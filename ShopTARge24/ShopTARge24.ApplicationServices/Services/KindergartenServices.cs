using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge24Context _context;

        public KindergartenServices(ShopTARge24Context context)
        {
            _context = context;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            var entity = new Kindergarten
            {
                Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                GroupName = dto.GroupName?.Trim(),
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName?.Trim(),
                TeacherName = dto.TeacherName?.Trim(),
                CreatedAt        = DateTime.UtcNow,
                UpdatedAt        = DateTime.UtcNow
            };

            await _context.Kindergarten.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            var entity = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null) return null;

            entity.GroupName = dto.GroupName?.Trim();
            entity.ChildrenCount = dto.ChildrenCount;
            entity.KindergartenName = dto.KindergartenName?.Trim();
            entity.TeacherName = dto.TeacherName?.Trim();
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var entity = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return null;

            var images = await _context.FileToApis
                .Where(f => f.KindergartenId == id)
                .ToListAsync();

            if (images.Count > 0)
            {
                _context.FileToApis.RemoveRange(images);
            }

            _context.Kindergarten.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            return await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
