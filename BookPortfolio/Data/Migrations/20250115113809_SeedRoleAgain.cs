using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoleAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9196e0d0-57e7-4c81-9cc4-c044ea400421");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdda6e24-f950-44b5-bbc2-373e8b15f267");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5cc31fcf-f870-46a2-b8ce-f9c67c654bd0", null, "Admin", "ADMIN" },
                    { "c42ca443-37ee-4fd4-ac80-8050e5e7f30a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cc31fcf-f870-46a2-b8ce-f9c67c654bd0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c42ca443-37ee-4fd4-ac80-8050e5e7f30a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9196e0d0-57e7-4c81-9cc4-c044ea400421", null, "Admin", "ADMIN" },
                    { "bdda6e24-f950-44b5-bbc2-373e8b15f267", null, "User", "USER" }
                });
        }
    }
}
