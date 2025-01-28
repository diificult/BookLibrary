using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "a8b5888f-e907-4112-b35f-3f787e5aeff1", null, "Admin", "ADMIN" },
                    { "ba0bf73e-9599-4bd0-91c8-a8932a878403", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8b5888f-e907-4112-b35f-3f787e5aeff1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba0bf73e-9599-4bd0-91c8-a8932a878403");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5cc31fcf-f870-46a2-b8ce-f9c67c654bd0", null, "Admin", "ADMIN" },
                    { "c42ca443-37ee-4fd4-ac80-8050e5e7f30a", null, "User", "USER" }
                });
        }
    }
}
