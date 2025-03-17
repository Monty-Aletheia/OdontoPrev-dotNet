using DentistService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DentistService.Infra.Data
{
    public class FIAPDbContext(DbContextOptions<FIAPDbContext> options) : DbContext(options)
    {

        public DbSet<Dentist> Dentists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Dentist>()
                .Property(d => d.RiskStatus)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
