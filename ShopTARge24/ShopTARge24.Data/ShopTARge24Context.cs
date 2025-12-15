using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using ShopTARge24.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ShopTARge24.Data
{
    public class ShopTARge24Context : IdentityDbContext<ApplicationUser>
    {
        public ShopTARge24Context(DbContextOptions<ShopTARge24Context> options)
            : base(options) { }

        public DbSet<Spaceships> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
        public DbSet<Kindergarten> Kindergarten { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        //public DbSet<IdentityDbContext> IdentityRoles { get; set; }
    }
}
