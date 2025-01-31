using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "489371c6-aa4b-4057-8451-33e9593cefff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64679719-5f4a-4f11-aa76-6cc7d2ee399d");

            migrationBuilder.AddColumn<string>(
                name: "BookState",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Portfolios",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01acc053-1f0b-4988-becb-ffa694d3450b", null, "Admin", "ADMIN" },
                    { "2fc56cf1-ee0c-483b-8ba8-4fae89cb4759", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01acc053-1f0b-4988-becb-ffa694d3450b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fc56cf1-ee0c-483b-8ba8-4fae89cb4759");

            migrationBuilder.DropColumn(
                name: "BookState",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Portfolios");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "489371c6-aa4b-4057-8451-33e9593cefff", null, "Admin", "ADMIN" },
                    { "64679719-5f4a-4f11-aa76-6cc7d2ee399d", null, "User", "USER" }
                });
        }
    }
}
