using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEnumForAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Status",
                table: "Appointments",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a06094a-2c0e-4c86-868c-0b02722504f2", null, "AdminOfHC", null },
                    { "5047958f-7ebd-454d-afcb-74ed4a21d3e8", null, "AdminOfMinistryOfHealth", null },
                    { "b80ad8a6-30ef-47f5-bacc-55b12eea3def", null, "Doctor", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a06094a-2c0e-4c86-868c-0b02722504f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5047958f-7ebd-454d-afcb-74ed4a21d3e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b80ad8a6-30ef-47f5-bacc-55b12eea3def");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

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
    }
}
