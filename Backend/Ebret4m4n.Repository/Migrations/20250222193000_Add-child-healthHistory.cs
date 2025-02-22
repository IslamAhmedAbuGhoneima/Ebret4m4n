using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddchildhealthHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "080ae79d-234b-4705-893c-c39f7372e0d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ae13225-48c0-4588-98ee-99538cf08d16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d96aef87-1fad-423c-9dc4-5b81440d4fb7");

            migrationBuilder.AddColumn<string>(
                name: "PatientHistory",
                table: "Children",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a35fa06-8114-4849-b60c-4bda78ce0213", null, "Doctor", null },
                    { "5ba49944-2ba5-4706-8a09-c0e25c60a6cf", null, "AdminOfMinistryOfHealth", null },
                    { "db0547c2-999e-4b71-b359-662c6e404048", null, "AdminOfHC", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a35fa06-8114-4849-b60c-4bda78ce0213");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ba49944-2ba5-4706-8a09-c0e25c60a6cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db0547c2-999e-4b71-b359-662c6e404048");

            migrationBuilder.DropColumn(
                name: "PatientHistory",
                table: "Children");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "080ae79d-234b-4705-893c-c39f7372e0d0", null, "AdminOfHC", null },
                    { "1ae13225-48c0-4588-98ee-99538cf08d16", null, "Doctor", null },
                    { "d96aef87-1fad-423c-9dc4-5b81440d4fb7", null, "AdminOfMinistryOfHealth", null }
                });
        }
    }
}
