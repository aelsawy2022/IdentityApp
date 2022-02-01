using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Persistance.Data.Migrations
{
    public partial class ApplySeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5988197a-115a-4bdc-be68-dac56210b258"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ea2f2fe-ce68-4c54-b3c9-2c63bef92643"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("3e9b022c-b1c1-4a8f-be59-d6f7d11b5f3a"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("5c15d3f0-0748-4ea2-97ba-a947314c01c1"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("2b5996a3-65a6-4ccb-b3f0-a9656e53b55b"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("417eb4c5-98a8-4ad2-8f42-9b810f8e1d55"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("47428a0f-6c6c-474a-935c-c56e943b6784"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Active", "ActivityId", "ConcurrencyStamp", "Name", "NormalizedName", "SchoolId" },
                values: new object[,]
                {
                    { new Guid("b87c04ec-05ae-46bc-9822-1b71d5ba54ba"), true, null, "5ef3c762-e074-4d42-b8dc-1c2c57f29477", "Admin", "ADMIN", null },
                    { new Guid("260365fd-1e85-4c62-a794-ffe3c4dc4215"), true, null, "94ff9e37-6e70-47fb-b586-00dfa63a1977", "SuperAdmin", "SUPERADMIN", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "Email", "EmailConfirmed", "GovernorateId", "Image", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("75eb25d7-6f73-4281-a01e-a5ee8208fdd4"), 0, true, "b9bf9aea-a77d-42f6-b466-a8ce0221b807", "admin@school.com", true, null, null, true, null, "Admin", "ADMIN@SCHOOL.COM", "ADMIN@SCHOOL.COM", "AQAAAAEAACcQAAAAEBufQAQJbYDau/j+n+KO6uup6jdG4PwIXKoCyUCE3ctCHNDSJkWl5U4HJxmNIJ6EEw==", null, false, "MHERALYVWRDCTGRJYR4MHFEK77FFQ6JU", false, "admin@school.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    {new Guid("75eb25d7-6f73-4281-a01e-a5ee8208fdd4"), new Guid("b87c04ec-05ae-46bc-9822-1b71d5ba54ba") },
                    {new Guid("75eb25d7-6f73-4281-a01e-a5ee8208fdd4"), new Guid("260365fd-1e85-4c62-a794-ffe3c4dc4215") }
                });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("586be096-48ef-44d7-94e4-2d0a0c06302a"), new DateTime(2022, 1, 25, 18, 7, 2, 922, DateTimeKind.Local).AddTicks(9530), "Cairo" },
                    { new Guid("e6a4960f-5dce-4c6f-b5e1-b5d146f3ea7a"), new DateTime(2022, 1, 25, 18, 7, 2, 922, DateTimeKind.Local).AddTicks(9549), "Giza" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Active", "CreationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("61127563-eadd-495e-b334-3c427cc6f85b"), true, new DateTime(2022, 1, 25, 18, 7, 2, 922, DateTimeKind.Local).AddTicks(1354), "Student" },
                    { new Guid("edd32a5d-b776-4b06-a880-a084a6bec2e2"), true, new DateTime(2022, 1, 25, 18, 7, 2, 922, DateTimeKind.Local).AddTicks(8627), "Teacher" },
                    { new Guid("f44ed6f2-0c3e-4184-abee-97fa9e8cbdc1"), true, new DateTime(2022, 1, 25, 18, 7, 2, 922, DateTimeKind.Local).AddTicks(8644), "Manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("260365fd-1e85-4c62-a794-ffe3c4dc4215"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b87c04ec-05ae-46bc-9822-1b71d5ba54ba"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("75eb25d7-6f73-4281-a01e-a5ee8208fdd4"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("586be096-48ef-44d7-94e4-2d0a0c06302a"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("e6a4960f-5dce-4c6f-b5e1-b5d146f3ea7a"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("61127563-eadd-495e-b334-3c427cc6f85b"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("edd32a5d-b776-4b06-a880-a084a6bec2e2"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("f44ed6f2-0c3e-4184-abee-97fa9e8cbdc1"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Active", "ActivityId", "ConcurrencyStamp", "Name", "NormalizedName", "SchoolId" },
                values: new object[,]
                {
                    { new Guid("7ea2f2fe-ce68-4c54-b3c9-2c63bef92643"), true, null, "9e902f61-1360-47ca-85bd-a66b556d02ae", "Admin", "ADMIN", null },
                    { new Guid("5988197a-115a-4bdc-be68-dac56210b258"), true, null, "dbc36c74-a8f3-48b4-bdd2-a79d7d054cf6", "SuperAdmin", "SUPERADMIN", null }
                });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("3e9b022c-b1c1-4a8f-be59-d6f7d11b5f3a"), new DateTime(2022, 1, 25, 17, 44, 1, 504, DateTimeKind.Local).AddTicks(3773), "Cairo" },
                    { new Guid("5c15d3f0-0748-4ea2-97ba-a947314c01c1"), new DateTime(2022, 1, 25, 17, 44, 1, 504, DateTimeKind.Local).AddTicks(3797), "Giza" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Active", "CreationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("417eb4c5-98a8-4ad2-8f42-9b810f8e1d55"), true, new DateTime(2022, 1, 25, 17, 44, 1, 503, DateTimeKind.Local).AddTicks(2991), "Student" },
                    { new Guid("2b5996a3-65a6-4ccb-b3f0-a9656e53b55b"), true, new DateTime(2022, 1, 25, 17, 44, 1, 504, DateTimeKind.Local).AddTicks(2716), "Teacher" },
                    { new Guid("47428a0f-6c6c-474a-935c-c56e943b6784"), true, new DateTime(2022, 1, 25, 17, 44, 1, 504, DateTimeKind.Local).AddTicks(2737), "Manager" }
                });
        }
    }
}
