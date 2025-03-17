using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientService.Migrations
{
    /// <inheritdoc />
    public partial class createPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    risk_status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    consultation_frequency = table.Column<int>(type: "int", nullable: true),
                    associated_claims = table.Column<string>(type: "CLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_patient", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_patient");
        }
    }
}
