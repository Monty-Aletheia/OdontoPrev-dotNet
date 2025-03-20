using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ClaimService.Migrations
{
	/// <inheritdoc />
	public partial class createClaim : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "tb_claims");
		}
	}
}