using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gleb.Migrations
{
    /// <inheritdoc />
    public partial class remove_subject_in_teacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_SchoolSubjects_SchoolSubjectId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SchoolSubjectId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SchoolSubjectId",
                table: "Teachers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolSubjectId",
                table: "Teachers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SchoolSubjectId",
                table: "Teachers",
                column: "SchoolSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_SchoolSubjects_SchoolSubjectId",
                table: "Teachers",
                column: "SchoolSubjectId",
                principalTable: "SchoolSubjects",
                principalColumn: "Id");
        }
    }
}
