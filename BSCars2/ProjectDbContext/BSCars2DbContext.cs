using BSCars2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BSCars2.ProjectDbContext
{
    public class BSCars2DbContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<LikedCars> LikedCars { get; set; }
        public BSCars2DbContext(DbContextOptions<BSCars2DbContext> options) : base(options)
        {

        }
    }
}
