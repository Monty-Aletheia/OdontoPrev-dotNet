using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentistService.Migrations
{
	/// <inheritdoc />
	public partial class updateDentistEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Password",
				table: "tb_dentist",
				type: "NVARCHAR2(2000)",
				nullable: false,
				defaultValue: "");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Password",
				table: "tb_dentist");
		}
	}
}