using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class publicPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad071dbc-ddee-4d58-999f-9906849cea73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5f35378-c0c8-4c56-9f2e-681968e02e5f");

            migrationBuilder.AddColumn<bool>(
                name: "isPortfolioPublic",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92c36760-298e-48c7-b0cd-0fba03bce197", null, "Admin", "ADMIN" },
                    { "a2a4ccbd-a25c-4329-95cb-352867d2bacf", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92c36760-298e-48c7-b0cd-0fba03bce197");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2a4ccbd-a25c-4329-95cb-352867d2bacf");

            migrationBuilder.DropColumn(
                name: "isPortfolioPublic",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad071dbc-ddee-4d58-999f-9906849cea73", null, "User", "USER" },
                    { "e5f35378-c0c8-4c56-9f2e-681968e02e5f", null, "Admin", "ADMIN" }
                });
        }
    }
}
