using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c4c7d47-8380-49e4-a71e-29b89ff475b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4df887a-5b6f-469b-996f-3b721bc13e45");

            migrationBuilder.DropColumn(
                name: "YearRelease",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "genre",
                table: "Books",
                newName: "coverIds");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN_10",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ISBN_13",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishDate",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45b2b700-6c3e-40e8-a91d-2f954779c3b2", null, "Admin", "ADMIN" },
                    { "ee8f4e2e-fb40-4a42-a3bb-5fe0ce7226be", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45b2b700-6c3e-40e8-a91d-2f954779c3b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee8f4e2e-fb40-4a42-a3bb-5fe0ce7226be");

            migrationBuilder.DropColumn(
                name: "ISBN_13",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "coverIds",
                table: "Books",
                newName: "genre");

            migrationBuilder.AlterColumn<int>(
                name: "ISBN_10",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearRelease",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5c4c7d47-8380-49e4-a71e-29b89ff475b0", null, "Admin", "ADMIN" },
                    { "f4df887a-5b6f-469b-996f-3b721bc13e45", null, "User", "USER" }
                });
        }
    }
}
