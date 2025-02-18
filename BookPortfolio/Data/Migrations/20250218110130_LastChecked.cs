using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class LastChecked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01acc053-1f0b-4988-becb-ffa694d3450b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fc56cf1-ee0c-483b-8ba8-4fae89cb4759");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad071dbc-ddee-4d58-999f-9906849cea73", null, "User", "USER" },
                    { "e5f35378-c0c8-4c56-9f2e-681968e02e5f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad071dbc-ddee-4d58-999f-9906849cea73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5f35378-c0c8-4c56-9f2e-681968e02e5f");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01acc053-1f0b-4988-becb-ffa694d3450b", null, "Admin", "ADMIN" },
                    { "2fc56cf1-ee0c-483b-8ba8-4fae89cb4759", null, "User", "USER" }
                });
        }
    }
}
