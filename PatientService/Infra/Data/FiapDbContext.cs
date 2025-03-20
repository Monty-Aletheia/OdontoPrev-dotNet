using Microsoft.EntityFrameworkCore;
using PatientService.Domain.Models;

namespace PatientService.Infra.Data
{
	public class FIAPDbContext(DbContextOptions<FIAPDbContext> options) : DbContext(options)
	{

		public DbSet<Patient> Patients { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

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