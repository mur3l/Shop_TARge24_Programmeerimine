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
            //Kontrolli ChildrenCount "Ingvar Testi jaoks"
            Validate(dto);

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
            //Kontrolli ChildrenCount "Ingvar Testi jaoks"
            Validate(dto);

            var entity = await _context
                .Kindergarten
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
                return null;

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
            var entity = await _context
                .Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

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

        //Kontrolli meetod
        public void Validate(KindergartenDto dto)
        {
            if (dto == null)
                throw new ArgumentException("Palun sisesta andmed.");

            if (string.IsNullOrWhiteSpace(dto.GroupName))
                throw new ArgumentException("Groupname on kohustuslik.");

            if(string.IsNullOrWhiteSpace(dto.KindergartenName))
                throw new ArgumentException("KindergartenName on kohustuslik.");

            if(string.IsNullOrWhiteSpace(dto.TeacherName))
                throw new ArgumentException("TeacherName on kohustuslik.");

            if (dto.ChildrenCount <= 0)
                throw new ArgumentException("ChildrenCount peab olema suurem kui null.");
        }
    }
}
