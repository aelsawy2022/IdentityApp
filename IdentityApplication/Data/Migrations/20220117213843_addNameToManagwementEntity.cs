using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityApplication.Data.Migrations
{
    public partial class addNameToManagwementEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Managements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Managements");
        }
    }
}
