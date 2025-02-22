using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updatechildhealthHistoryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PatientHistory",
                table: "Children",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "389f6f82-ad6f-4f59-b083-169b4a54c9f1", null, "AdminOfMinistryOfHealth", null },
                    { "654fb45c-aa85-402b-9bec-57f8a6905dea", null, "AdminOfHC", null },
                    { "67cb5f09-35ca-4d5a-ae7a-ad9cb8ffaed8", null, "Doctor", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "389f6f82-ad6f-4f59-b083-169b4a54c9f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "654fb45c-aa85-402b-9bec-57f8a6905dea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67cb5f09-35ca-4d5a-ae7a-ad9cb8ffaed8");

            migrationBuilder.AlterColumn<string>(
                name: "PatientHistory",
                table: "Children",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500,
                oldNullable: true);

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
    }
}
