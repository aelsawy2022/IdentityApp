using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Persistance.Data.Migrations
{
    public partial class addNewEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "ActivityInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ActivityId = table.Column<Guid>(nullable: false),
                    SeasonId = table.Column<Guid>(nullable: false),
                    InstanceDate = table.Column<DateTime>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityInstances_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityInstances_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityInstanceDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    InstanceId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ActivityInstanceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityInstanceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityInstanceDetails_ActivityInstances_ActivityInstanceId",
                        column: x => x.ActivityInstanceId,
                        principalTable: "ActivityInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityInstanceDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstanceDetails_ActivityInstanceId",
                table: "ActivityInstanceDetails",
                column: "ActivityInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstanceDetails_UserId",
                table: "ActivityInstanceDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_ActivityId",
                table: "ActivityInstances",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_SeasonId",
                table: "ActivityInstances",
                column: "SeasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityInstanceDetails");

            migrationBuilder.DropTable(
                name: "ActivityInstances");

        }
    }
}
