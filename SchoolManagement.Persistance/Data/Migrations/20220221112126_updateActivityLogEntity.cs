using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Persistance.Data.Migrations
{
    public partial class updateActivityLogEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "ActivityLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "ActivityLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "ActivityLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ActivityLogs",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "Action",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "Controller",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ActivityLogs");
            
        }
    }
}
