using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;

namespace ShopTARge24.Data
{
    public class KindergartenContext : DbContext
    {
        public KindergartenContext(DbContextOptions<KindergartenContext> options) 
            : base(options){}
        public DbSet<Kindergarten> Kindergarten { get; set; } 
    }
}
