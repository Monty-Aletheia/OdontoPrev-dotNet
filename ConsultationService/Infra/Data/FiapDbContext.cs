using ConsultationService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService.Infra.Data
{
    public class FIAPDbContext : DbContext
    {
        public FIAPDbContext(DbContextOptions<FIAPDbContext> options) : base(options)
        {
        }

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<ConsultationDentist> ConsultationDentists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultation>()
                 .Property(c => c.RiskStatus)
                 .HasConversion<string>();

            modelBuilder.Entity<ConsultationDentist>()
                 .HasKey(cd => new { cd.ConsultationId, cd.DentistId });

            modelBuilder.Entity<ConsultationDentist>()
                .HasOne(cd => cd.Consultation)
                .WithMany(c => c.ConsultationDentists)
                .HasForeignKey(cd => cd.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Removido o relacionamento com o Dentist, pois ele não existe nesse microsserviço.
            modelBuilder.Entity<ConsultationDentist>()
                .HasOne<Consultation>()  // Não há relacionamento com a entidade Dentist no microsserviço.
                .WithMany()
                .HasForeignKey(cd => cd.DentistId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar deleção em cascata de Dentists

            base.OnModelCreating(modelBuilder);
        }
    }
}
