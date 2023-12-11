using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTime.Migrations
{
    /// <inheritdoc />
    public partial class NewMigForAddValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7451c6df-6742-4068-b886-dc0fbe19ec59");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9078b9d2-471c-427b-870f-532e3d58e530");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e07f3d15-4c6d-4c62-bf25-cec2e72cc485", null, "USER", "USER" },
                    { "e8f53f88-78c3-4384-b3fe-8b5c9ff32637", null, "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e07f3d15-4c6d-4c62-bf25-cec2e72cc485");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8f53f88-78c3-4384-b3fe-8b5c9ff32637");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7451c6df-6742-4068-b886-dc0fbe19ec59", null, "ADMIN", "ADMIN" },
                    { "9078b9d2-471c-427b-870f-532e3d58e530", null, "USER", "USER" }
                });
        }
    }
}
