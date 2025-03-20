﻿// <auto-generated />
using System;
using ConsultationService.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace ConsultationService.Migrations
{
    [DbContext(typeof(FIAPDbContext))]
    [Migration("20250319232450_fixConsultationDentist")]
    partial class fixConsultationDentist
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConsultationService.Domain.Models.Consultation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<DateTime>("ConsultationDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("consultation_date");

                    b.Property<double>("ConsultationValue")
                        .HasColumnType("BINARY_DOUBLE")
                        .HasColumnName("consultation_value");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("RAW(16)")
                        .HasColumnName("patient_id");

                    b.Property<string>("RiskStatus")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("risk_status");

                    b.HasKey("Id");

                    b.ToTable("tb_consultations");
                });

            modelBuilder.Entity("ConsultationService.Domain.Models.ConsultationDentist", b =>
                {
                    b.Property<Guid>("ConsultationId")
                        .HasColumnType("RAW(16)");

                    b.Property<Guid>("DentistId")
                        .HasColumnType("RAW(16)")
                        .HasColumnName("dentist_id");

                    b.Property<Guid>("Id")
                        .HasColumnType("RAW(16)");

                    b.HasKey("ConsultationId", "DentistId");

                    b.ToTable("tb_consultation_dentists");
                });

            modelBuilder.Entity("ConsultationService.Domain.Models.ConsultationDentist", b =>
                {
                    b.HasOne("ConsultationService.Domain.Models.Consultation", "Consultation")
                        .WithMany("ConsultationDentists")
                        .HasForeignKey("ConsultationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consultation");
                });

            modelBuilder.Entity("ConsultationService.Domain.Models.Consultation", b =>
                {
                    b.Navigation("ConsultationDentists");
                });
#pragma warning restore 612, 618
        }
    }
}
