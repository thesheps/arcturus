using Arcturus.Domain;
using System.Data.Entity;

namespace Arcturus.Concrete
{
    public class LicensingDbContext : DbContext
    {
        public DbSet<License> License { get; set; }

        public LicensingDbContext(string connectionString)
            :base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<License>().ToTable("License");
        }
    }
}