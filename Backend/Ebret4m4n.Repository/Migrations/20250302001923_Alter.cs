using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Alter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "191c486e-4213-4a1f-976c-85733ab7c7c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74190ebc-ce16-4e92-b6a2-6c62399bfcf5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e22ec3e-f1a5-4cdd-b251-d35775da1291");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca6dab54-bd3d-4839-89b6-2403fb4249c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df51f641-df73-4f92-8054-8a49c478f205");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("071b99da-d916-4a3b-af54-f799a686660b"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("218c1c44-4e58-4529-905e-41bd845a2a6f"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("33708959-1091-42fe-937e-c92b67e59934"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("64a2982b-d1b3-4248-ad4f-8ca4d8a39976"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("b8e0b40b-6440-4fc6-bbab-7f823a9325b9"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("cfcaae8a-bcda-4583-9481-dc9016c52a4f"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("facf0c6c-6e01-4558-af73-3b30655318e7"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0776836b-d2a8-4df1-a04b-c90bf970c74b", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "347f6ecb-7ee1-4ca0-a192-2e8459daa32e", null, "cityAdmin", "CITYADMIN" },
                    { "42d8ffc8-2421-4ddd-a0ba-555da62d8d2b", null, "organizer", "ORGANIZER" },
                    { "8abeeb78-978a-4f42-a5d8-b286ba62377b", null, "admin", "ADMIN" },
                    { "f8299b7d-52c7-4139-8678-65417d8b715e", null, "doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("098b3289-d9e7-4734-8fba-60a053158119"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("4692bcdc-02be-4bf6-9a21-37c0a98cbef7"), "بني مزار", "Sunday", "المنيا", "الوحده المحليه بقريه صفط ابوجرج", "Wednesday", "صفط ابوجرج" },
                    { new Guid("7aae37c7-d0ff-4498-bd9d-6d8bcfc1cfd9"), "عين شمس", "Sunday", "القاهره", "الوحده المحليه بالقاهره", "Tuesday", "عين شمس" },
                    { new Guid("8fe483aa-9254-41a9-b205-efcfd1908fa8"), "الجيزه", "Sunday", "الجيزه", "الوحده المحليه بالجيزه", "Wednesday", "منيل الروضه" },
                    { new Guid("e47d4230-3e1f-4a3f-81a8-ed6be269bfa7"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("e6f30f14-9bbc-4e4e-a452-1c773505f561"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("fcc953df-adfe-460c-bcf9-dff089eff0d8"), "العدوي", "Sunday", "المنيا", "الوحده المحليه بالعدوي", "Wednesday", "العدوي" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0776836b-d2a8-4df1-a04b-c90bf970c74b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "347f6ecb-7ee1-4ca0-a192-2e8459daa32e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42d8ffc8-2421-4ddd-a0ba-555da62d8d2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8abeeb78-978a-4f42-a5d8-b286ba62377b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8299b7d-52c7-4139-8678-65417d8b715e");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("098b3289-d9e7-4734-8fba-60a053158119"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("4692bcdc-02be-4bf6-9a21-37c0a98cbef7"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("7aae37c7-d0ff-4498-bd9d-6d8bcfc1cfd9"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("8fe483aa-9254-41a9-b205-efcfd1908fa8"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("e47d4230-3e1f-4a3f-81a8-ed6be269bfa7"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("e6f30f14-9bbc-4e4e-a452-1c773505f561"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("fcc953df-adfe-460c-bcf9-dff089eff0d8"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "191c486e-4213-4a1f-976c-85733ab7c7c5", null, "cityAdmin", "CITYADMIN" },
                    { "74190ebc-ce16-4e92-b6a2-6c62399bfcf5", null, "admin", "ADMIN" },
                    { "8e22ec3e-f1a5-4cdd-b251-d35775da1291", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "ca6dab54-bd3d-4839-89b6-2403fb4249c4", null, "organizer", "ORGANIZER" },
                    { "df51f641-df73-4f92-8054-8a49c478f205", null, "doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("071b99da-d916-4a3b-af54-f799a686660b"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("218c1c44-4e58-4529-905e-41bd845a2a6f"), "العدوي", "Sunday", "المنيا", "الوحده المحليه بالعدوي", "Wednesday", "العدوي" },
                    { new Guid("33708959-1091-42fe-937e-c92b67e59934"), "بني مزار", "Sunday", "المنيا", "الوحده المحليه بقريه صفط ابوجرج", "Wednesday", "صفط ابوجرج" },
                    { new Guid("64a2982b-d1b3-4248-ad4f-8ca4d8a39976"), "عين شمس", "Sunday", "القاهره", "الوحده المحليه بالقاهره", "Tuesday", "عين شمس" },
                    { new Guid("b8e0b40b-6440-4fc6-bbab-7f823a9325b9"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("cfcaae8a-bcda-4583-9481-dc9016c52a4f"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("facf0c6c-6e01-4558-af73-3b30655318e7"), "الجيزه", "Sunday", "الجيزه", "الوحده المحليه بالجيزه", "Wednesday", "منيل الروضه" }
                });
        }
    }
}
