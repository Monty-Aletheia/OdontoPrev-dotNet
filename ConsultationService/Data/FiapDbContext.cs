using ConsultationService.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService.Data
{
    public class FIAPDbContext(DbContextOptions<FIAPDbContext> options) : DbContext(options)
    {

        public DbSet<Consultation> Consultations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Consultation>()
                 .Property(c => c.RiskStatus)
                 .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
