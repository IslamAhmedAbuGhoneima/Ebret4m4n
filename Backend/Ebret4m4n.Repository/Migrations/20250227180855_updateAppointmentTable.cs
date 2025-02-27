using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d8e3752-bc9e-4a48-9bc9-bd1e0c7d18e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dcaae69-13a6-4ce3-8d4c-98eea01a9112");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc04f205-a820-4b74-9ef7-95e26fcb9e82");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("0a1ef8fa-0f95-4903-ad5c-f0f3bb2a4caf"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("5592b36d-24d8-4563-9dd8-9bb23e42453f"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("99944794-fc42-44dd-9d09-5ea58937d602"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0338d90f-1723-46be-9a59-a1dfb1083441", null, "nurse", "NURSE" },
                    { "8192e45a-44df-459b-869e-3384cf01a8ff", null, "Admin", "ADMIN" },
                    { "86c9850c-0513-49ad-bcf5-5406d5556ae8", null, "doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("2011f890-53e5-4916-97a2-90a9fd3e14c8"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("7f196ab7-5cb6-4e23-bbe3-dc5549c234a6"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("fa48413e-c673-4858-94ff-fb9ca8f4a8ff"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0338d90f-1723-46be-9a59-a1dfb1083441");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8192e45a-44df-459b-869e-3384cf01a8ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86c9850c-0513-49ad-bcf5-5406d5556ae8");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("2011f890-53e5-4916-97a2-90a9fd3e14c8"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("7f196ab7-5cb6-4e23-bbe3-dc5549c234a6"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("fa48413e-c673-4858-94ff-fb9ca8f4a8ff"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7d8e3752-bc9e-4a48-9bc9-bd1e0c7d18e0", null, "nurse", "NURSE" },
                    { "8dcaae69-13a6-4ce3-8d4c-98eea01a9112", null, "Admin", "ADMIN" },
                    { "fc04f205-a820-4b74-9ef7-95e26fcb9e82", null, "doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("0a1ef8fa-0f95-4903-ad5c-f0f3bb2a4caf"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("5592b36d-24d8-4563-9dd8-9bb23e42453f"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("99944794-fc42-44dd-9d09-5ea58937d602"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" }
                });
        }
    }
}
