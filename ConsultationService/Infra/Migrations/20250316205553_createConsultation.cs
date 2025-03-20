using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ConsultationService.Migrations
{
	/// <inheritdoc />
	public partial class createConsultation : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "tb_consultations");
		}
	}
}