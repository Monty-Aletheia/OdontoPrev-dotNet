using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aletheia.Infra.Migrations
{
    /// <inheritdoc />
    public partial class setupDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_dentist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Specialty = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    registration_number = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    claims_rate = table.Column<double>(type: "BINARY_DOUBLE", nullable: true),
                    risk_status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_dentist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    risk_status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    consultation_frequency = table.Column<int>(type: "NUMBER", nullable: true),
                    associated_claims = table.Column<string>(type: "CLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_consultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    consultation_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    consultation_value = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    risk_status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PatientId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_consultations_tb_patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "tb_patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationDentist",
                columns: table => new
                {
                    ConsultationsId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    DentistsId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationDentist", x => new { x.ConsultationsId, x.DentistsId });
                    table.ForeignKey(
                        name: "FK_ConsultationDentist_tb_consultations_ConsultationsId",
                        column: x => x.ConsultationsId,
                        principalTable: "tb_consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultationDentist_tb_dentist_DentistsId",
                        column: x => x.DentistsId,
                        principalTable: "tb_dentist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_claims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    occurrence_date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    claim_type = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    suggested_preventive_action = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_claims_tb_consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "tb_consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationDentist_DentistsId",
                table: "ConsultationDentist",
                column: "DentistsId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_claims_ConsultationId",
                table: "tb_claims",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consultations_PatientId",
                table: "tb_consultations",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultationDentist");

            migrationBuilder.DropTable(
                name: "tb_claims");

            migrationBuilder.DropTable(
                name: "tb_dentist");

            migrationBuilder.DropTable(
                name: "tb_consultations");

            migrationBuilder.DropTable(
                name: "tb_patient");
        }
    }
}
