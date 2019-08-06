using Microsoft.EntityFrameworkCore.Migrations;

namespace MockProject.Migrations
{
    public partial class facultyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGraduated",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Faculties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Faculties");

            migrationBuilder.AddColumn<bool>(
                name: "IsGraduated",
                table: "Users",
                nullable: true);
        }
    }
}
