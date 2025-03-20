using ClaimService.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class FIAPDbContext : DbContext
{
	public FIAPDbContext(DbContextOptions<FIAPDbContext> options) : base(options) { }

	public DbSet<Claim> Claims { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Claim>()
			.Property(c => c.ClaimType)
			.HasConversion<string>();

		base.OnModelCreating(modelBuilder);
	}
}