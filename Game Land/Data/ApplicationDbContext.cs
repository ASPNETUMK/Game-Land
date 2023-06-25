using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TNAI.Model.Entities;
using Game_Land.Entities;
using TNAI.Model.Configurations;

namespace Game_Land.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ConfigurationPay());
            modelBuilder.ApplyConfiguration(new Configuration_gry());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TNAI.Model.Entities.Gry>? Gry { get; set; }
        public DbSet<Game_Land.Entities.pay>? pay { get; set; }
    }
}