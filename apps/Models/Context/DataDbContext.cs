using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Context
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<PromoMaster> PromoMaster { get; set; }
        public DbSet<PromoTransaction> PromoTransaction { get; set; }
        public DbSet<PromoMaster> PromoTransactionDetail { get; set; }
    }
}
