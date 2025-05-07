using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultationService.Migrations
{
	/// <inheritdoc />
	public partial class fixconsultation : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "tb_consultations",
				type: "NVARCHAR2(2000)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "NVARCHAR2(2000)");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "tb_consultations",
				type: "NVARCHAR2(2000)",
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "NVARCHAR2(2000)",
				oldNullable: true);
		}
	}
}