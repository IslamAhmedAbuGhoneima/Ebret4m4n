using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedingHealthCareCenterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42e5c00f-c1c1-419d-9b6a-6d1ed51afda6", null, "Doctor", null },
                    { "702f2511-d4dd-4602-8c57-471d2517ee86", null, "AdminOfHC", null },
                    { "9c0aa03b-6cbe-4a75-8826-83b65eaa25f9", null, "AdminOfMinistryOfHealth", null }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("d204490a-dbfe-4fa3-91db-668520040203"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("dc2a4f1c-52c6-4522-97be-f37df0aad6ec"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("ed45f30f-747e-46b1-9236-b34f9bbfeeed"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42e5c00f-c1c1-419d-9b6a-6d1ed51afda6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "702f2511-d4dd-4602-8c57-471d2517ee86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c0aa03b-6cbe-4a75-8826-83b65eaa25f9");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("d204490a-dbfe-4fa3-91db-668520040203"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("dc2a4f1c-52c6-4522-97be-f37df0aad6ec"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("ed45f30f-747e-46b1-9236-b34f9bbfeeed"));

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
    }
}
