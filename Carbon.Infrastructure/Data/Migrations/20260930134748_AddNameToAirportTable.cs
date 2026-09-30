using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToAirportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Airport",
                table: "Airport");

            migrationBuilder.RenameTable(
                name: "Airport",
                newName: "CarbonAirports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarbonAirports",
                table: "CarbonAirports",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarbonAirports",
                table: "CarbonAirports");

            migrationBuilder.RenameTable(
                name: "CarbonAirports",
                newName: "Airport");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Airport",
                table: "Airport",
                column: "Id");
        }
    }
}
