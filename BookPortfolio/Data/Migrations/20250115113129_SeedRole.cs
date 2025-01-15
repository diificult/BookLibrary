using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a3ccfca-91b4-4c25-9f29-1fd1acdc9f07");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d7017e4-de19-4c49-852b-70e346264a2b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9196e0d0-57e7-4c81-9cc4-c044ea400421", null, "Admin", "ADMIN" },
                    { "bdda6e24-f950-44b5-bbc2-373e8b15f267", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "7a3ccfca-91b4-4c25-9f29-1fd1acdc9f07", null, "Admin", "ADMIN" },
                    { "9d7017e4-de19-4c49-852b-70e346264a2b", null, "User", "USER" }
                });
        }
    }
}
