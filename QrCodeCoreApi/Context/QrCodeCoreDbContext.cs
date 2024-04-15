using Microsoft.EntityFrameworkCore;

namespace QrCodeCore.Models.Context
{
    public class QrCodeCoreDbContext : DbContext
    {
        public QrCodeCoreDbContext() { }

        public QrCodeCoreDbContext(DbContextOptions<QrCodeCoreDbContext> options) : base(options) { }

        public DbSet<SysUsers> TBL_USERS { get; set; }
        public DbSet<Business> TBL_BUSINESSES { get; set; }
        public DbSet<FoodTypes> TBL_FOOD_TYPES { get; set; }
        public DbSet<Foods> TBL_FOODS { get; set; }
        public DbSet<Types> TBL_TYPES { get; set; }
    }
}

