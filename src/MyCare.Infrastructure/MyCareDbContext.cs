using Microsoft.EntityFrameworkCore;
using MyCare.Infrastructure.Entities;

namespace MyCare.Infrastructure
{
    public class MyCareDbContext : DbContext
    {
        public MyCareDbContext(DbContextOptions<MyCareDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<MedicineModel> Medicines { get; set; }
    }
}
