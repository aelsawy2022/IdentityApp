using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityApplication.Data.Migrations
{
    public partial class addNameToSchoolEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Schools",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Schools");
        }
    }
}
