using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ConsultationService.Migrations
{
	/// <inheritdoc />
	public partial class addConsultationDentist : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "PatientId",
				table: "tb_consultations",
				newName: "patient_id");

			migrationBuilder.CreateTable(
				name: "tb_consultation_dentists",
				columns: table => new
				{
					ConsultationId = table.Column<Guid>(type: "RAW(16)", nullable: false),
					dentist_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
					Id = table.Column<Guid>(type: "RAW(16)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_tb_consultation_dentists", x => new { x.ConsultationId, x.dentist_id });
					table.ForeignKey(
						name: "FK_tb_consultation_dentists_tb_consultations_ConsultationId",
						column: x => x.ConsultationId,
						principalTable: "tb_consultations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_tb_consultation_dentists_tb_consultations_dentist_id",
						column: x => x.dentist_id,
						principalTable: "tb_consultations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_tb_consultation_dentists_dentist_id",
				table: "tb_consultation_dentists",
				column: "dentist_id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "tb_consultation_dentists");

			migrationBuilder.RenameColumn(
				name: "patient_id",
				table: "tb_consultations",
				newName: "PatientId");
		}
	}
}