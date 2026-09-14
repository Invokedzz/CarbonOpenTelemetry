using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarbonRoles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("7f3a9c21-6b84-4d17-a5e2-91c7b8f04d36"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "General role", "General" },
                    { new Guid("a84d2f63-91c7-4be5-b038-6e2a7f19c5d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin role", "Admin" },
                    { new Guid("c2e51a79-3f06-48bd-9c42-7a1e5d83f6b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Director role", "Director" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarbonRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f3a9c21-6b84-4d17-a5e2-91c7b8f04d36"));

            migrationBuilder.DeleteData(
                table: "CarbonRoles",
                keyColumn: "Id",
                keyValue: new Guid("a84d2f63-91c7-4be5-b038-6e2a7f19c5d4"));

            migrationBuilder.DeleteData(
                table: "CarbonRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2e51a79-3f06-48bd-9c42-7a1e5d83f6b0"));
        }
    }
}
