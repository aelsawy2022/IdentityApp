using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Persistance.Data.Migrations
{
    public partial class updateOnSlotEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "ActivitySlots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "ActivitySlots",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "ActivitySlots");

            migrationBuilder.DropColumn(
                name: "To",
                table: "ActivitySlots");
        }
    }
}
