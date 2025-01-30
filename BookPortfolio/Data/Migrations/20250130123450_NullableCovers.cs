using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class NullableCovers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45b2b700-6c3e-40e8-a91d-2f954779c3b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee8f4e2e-fb40-4a42-a3bb-5fe0ce7226be");

            migrationBuilder.AlterColumn<string>(
                name: "coverIds",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PublishDate",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "489371c6-aa4b-4057-8451-33e9593cefff", null, "Admin", "ADMIN" },
                    { "64679719-5f4a-4f11-aa76-6cc7d2ee399d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "489371c6-aa4b-4057-8451-33e9593cefff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64679719-5f4a-4f11-aa76-6cc7d2ee399d");

            migrationBuilder.AlterColumn<string>(
                name: "coverIds",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PublishDate",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45b2b700-6c3e-40e8-a91d-2f954779c3b2", null, "Admin", "ADMIN" },
                    { "ee8f4e2e-fb40-4a42-a3bb-5fe0ce7226be", null, "User", "USER" }
                });
        }
    }
}
