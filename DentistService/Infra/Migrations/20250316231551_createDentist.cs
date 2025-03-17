using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistService.Migrations
{
    /// <inheritdoc />
    public partial class createDentist : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_dentist");
        }
    }
}
