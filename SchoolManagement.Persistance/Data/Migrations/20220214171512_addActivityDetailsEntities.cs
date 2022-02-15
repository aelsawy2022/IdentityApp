using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Persistance.Data.Migrations
{
    public partial class addActivityDetailsEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityClasses",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityClasses", x => new { x.ActivityId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_ActivityClasses_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivitySlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Slot = table.Column<string>(nullable: true),
                    ActivityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitySlots_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityUserTypes",
                columns: table => new
                {
                    UserTypeId = table.Column<Guid>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityUserTypes", x => new { x.ActivityId, x.UserTypeId });
                    table.ForeignKey(
                        name: "FK_ActivityUserTypes_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityUserTypes_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityClasses_ClassId",
                table: "ActivityClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySlots_ActivityId",
                table: "ActivitySlots",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUserTypes_UserTypeId",
                table: "ActivityUserTypes",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityClasses");

            migrationBuilder.DropTable(
                name: "ActivitySlots");

            migrationBuilder.DropTable(
                name: "ActivityUserTypes");
        }
    }
}
