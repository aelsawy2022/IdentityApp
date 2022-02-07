using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Persistance.Data.Migrations
{
    public partial class addRefreshTokenToUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Active", "ActivityId", "ConcurrencyStamp", "Name", "NormalizedName", "SchoolId" },
                values: new object[,]
                {
                    { new Guid("7d1c61fd-5d09-4912-8772-fe186fb31dfe"), true, null, "a977d824-513f-4cf2-93d9-b6ad477b65ff", "Admin", "ADMIN", null },
                    { new Guid("0318ad62-270e-4c6b-b739-c4ec7aed1c06"), true, null, "26985aa6-c994-4816-ae28-34ff4ebd9fb7", "SuperAdmin", "SUPERADMIN", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "Email", "EmailConfirmed", "GovernorateId", "Image", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("74f61c70-2c76-43cf-8c6f-7b1b3803b49b"), 0, true, "b9bf9aea-a77d-42f6-b466-a8ce0221b807", "admin@school.com", true, null, null, true, null, "Admin", "ADMIN@SCHOOL.COM", "ADMIN@SCHOOL.COM", "AQAAAAEAACcQAAAAEBufQAQJbYDau/j+n+KO6uup6jdG4PwIXKoCyUCE3ctCHNDSJkWl5U4HJxmNIJ6EEw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MHERALYVWRDCTGRJYR4MHFEK77FFQ6JU", false, "admin@school.com" });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("a252f032-6f08-4250-9bff-1c7c3f6d09ef"), new DateTime(2022, 2, 6, 18, 47, 37, 677, DateTimeKind.Local).AddTicks(1141), "Cairo" },
                    { new Guid("66d492f8-d0de-4b71-8071-447537965b4a"), new DateTime(2022, 2, 6, 18, 47, 37, 677, DateTimeKind.Local).AddTicks(1169), "Giza" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Active", "CreationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("38328de5-58ec-4d6d-ae45-7049837adfa4"), true, new DateTime(2022, 2, 6, 18, 47, 37, 676, DateTimeKind.Local).AddTicks(2965), "Student" },
                    { new Guid("e6b2093a-76a7-4d70-b53d-004e8b6513d2"), true, new DateTime(2022, 2, 6, 18, 47, 37, 677, DateTimeKind.Local).AddTicks(411), "Teacher" },
                    { new Guid("c81b6b12-972c-4efb-a2ca-6b2c64650948"), true, new DateTime(2022, 2, 6, 18, 47, 37, 677, DateTimeKind.Local).AddTicks(443), "Manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0318ad62-270e-4c6b-b739-c4ec7aed1c06"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7d1c61fd-5d09-4912-8772-fe186fb31dfe"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("74f61c70-2c76-43cf-8c6f-7b1b3803b49b"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("66d492f8-d0de-4b71-8071-447537965b4a"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("a252f032-6f08-4250-9bff-1c7c3f6d09ef"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("38328de5-58ec-4d6d-ae45-7049837adfa4"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("c81b6b12-972c-4efb-a2ca-6b2c64650948"));

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: new Guid("e6b2093a-76a7-4d70-b53d-004e8b6513d2"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
