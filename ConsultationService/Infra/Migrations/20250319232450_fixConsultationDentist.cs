using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultationService.Migrations
{
	/// <inheritdoc />
	public partial class fixConsultationDentist : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_tb_consultation_dentists_tb_consultations_dentist_id",
				table: "tb_consultation_dentists");

			migrationBuilder.DropIndex(
				name: "IX_tb_consultation_dentists_dentist_id",
				table: "tb_consultation_dentists");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateIndex(
				name: "IX_tb_consultation_dentists_dentist_id",
				table: "tb_consultation_dentists",
				column: "dentist_id");

			migrationBuilder.AddForeignKey(
				name: "FK_tb_consultation_dentists_tb_consultations_dentist_id",
				table: "tb_consultation_dentists",
				column: "dentist_id",
				principalTable: "tb_consultations",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
	}
}