using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityApplication.Data.Migrations
{
    public partial class updateClassEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Classes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Classes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Classes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentImg",
                table: "Classes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherImg",
                table: "Classes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "StudentImg",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TeacherImg",
                table: "Classes");
        }
    }
}
