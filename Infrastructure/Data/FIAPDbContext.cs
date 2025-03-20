using Aletheia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Data
{
	public class FIAPDbContext(DbContextOptions<FIAPDbContext> options) : DbContext(options)
	{
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Dentist> Dentists { get; set; }
		public DbSet<Consultation> Consultations { get; set; }
		public DbSet<Claim> Claims { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Claim>()
				.Property(c => c.ClaimType)
				.HasConversion<string>();

			modelBuilder.Entity<Consultation>()
				.Property(c => c.RiskStatus)
				.HasConversion<string>();

			modelBuilder.Entity<Dentist>()
				.Property(d => d.RiskStatus)
				.HasConversion<string>();

			modelBuilder.Entity<Patient>()
				.Property(p => p.RiskStatus)
				.HasConversion<string>();

			modelBuilder.Entity<Patient>()
			   .Property(p => p.Gender)
			   .HasConversion<string>();

			base.OnModelCreating(modelBuilder);
		}
	}
}