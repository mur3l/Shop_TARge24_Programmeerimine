using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using ShopTARge24.Core.Domain;


namespace ShopTARge24.Data
{
    public class ShopTARge24Context : DbContext
    {
        public ShopTARge24Context(DbContextOptions<ShopTARge24Context> options)
            : base(options) { }

        public DbSet<Spaceships> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }

        public DbSet<FileToApi> RealEstates { get; set; }
    }

    public class ShopTARge24ContextFactory : IDesignTimeDbContextFactory<ShopTARge24Context>
    {
        public ShopTARge24Context CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ShopTARge24Context>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ShopTARge24Context(optionsBuilder.Options);
        }
    }
}
