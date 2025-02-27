using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3be7f495-4a53-447e-8295-b025f28bc03a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d147488-207b-4882-90fd-47199e17618d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62ed60ec-1b5e-4eff-8fa6-ff3a31a2b8be");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("44347d7e-1b87-42fd-b616-2fdfa121273b"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("6d36a91f-9f56-422d-9e23-12bb9799cf5d"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("d3e9a74c-ce72-4313-8196-9a3fc9a70735"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35bdbd0e-4e4d-447b-a0aa-c40eaa5cd928", null, "nurse", "NURSE" },
                    { "e188fe3e-ac62-4d99-9b46-43b0cab00624", null, "doctor", "DOCTOR" },
                    { "fdcb355c-601f-43c6-b70b-2780f3cccb84", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("0a978b88-004c-4249-b848-f88d28807faf"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("4198e7b3-5e1e-465d-ab46-1d797caa5ff7"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("a1712b69-81be-4e59-aa7b-61772493c1a8"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35bdbd0e-4e4d-447b-a0aa-c40eaa5cd928");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e188fe3e-ac62-4d99-9b46-43b0cab00624");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdcb355c-601f-43c6-b70b-2780f3cccb84");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("0a978b88-004c-4249-b848-f88d28807faf"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("4198e7b3-5e1e-465d-ab46-1d797caa5ff7"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("a1712b69-81be-4e59-aa7b-61772493c1a8"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3be7f495-4a53-447e-8295-b025f28bc03a", null, "AdminOfHC", null },
                    { "3d147488-207b-4882-90fd-47199e17618d", null, "Doctor", null },
                    { "62ed60ec-1b5e-4eff-8fa6-ff3a31a2b8be", null, "AdminOfMinistryOfHealth", null }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("44347d7e-1b87-42fd-b616-2fdfa121273b"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("6d36a91f-9f56-422d-9e23-12bb9799cf5d"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("d3e9a74c-ce72-4313-8196-9a3fc9a70735"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" }
                });
        }
    }
}
