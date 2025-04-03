using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addingRelationToHealthCareWithCityAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a645924-052a-4646-b998-c7013ab50bcb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb3a8bd-a507-4db1-8e6e-a5bea6044d4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ea04679-3639-4b33-90cd-5be55dc13822");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e741bc6-635b-43be-873e-32cdfec53bd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88dd76f5-7479-44db-b051-4f8152fb546c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff0a1ceb-ced4-46b4-a4c3-17939ac4b027");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("2a7d506e-8bf2-4a18-8cd0-a9eee4c75890"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("6759c327-e243-43d9-a84b-32d847a4d267"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("7fdc64ec-ebc4-498d-b3d1-8b30a73c1d01"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("8b2c98f2-ff92-4929-8927-7646b0138cd8"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("94d7a785-f17e-4f98-a0ff-fc36d75fcd7d"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("bc49ce92-a4a2-45f7-98e0-74aedc967622"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("bd62e348-7184-4848-968c-fe11f6639ed0"));

            migrationBuilder.AddColumn<string>(
                name: "CityAdminId",
                table: "HealthCareCenters",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsResolved",
                table: "Complaints",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chats",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350);

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e0efe84-8eef-4dae-a726-085ecc940340", null, "parent", "PARENT" },
                    { "608b0aa5-521f-4a29-86b7-91659cf99cce", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "acacc8ad-d2a5-44d0-83e0-989a7d0440d5", null, "admin", "ADMIN" },
                    { "e94a81b2-b746-4846-9fb0-4fc096b174d2", null, "cityAdmin", "CITYADMIN" },
                    { "f00bd245-6853-4e82-a671-f794fc10b752", null, "doctor", "DOCTOR" },
                    { "fa10bbaf-a1ec-403d-aa53-41631193e50f", null, "organizer", "ORGANIZER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthCareCenters_CityAdminId",
                table: "HealthCareCenters",
                column: "CityAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthCareCenters_CityAdminStaff_CityAdminId",
                table: "HealthCareCenters",
                column: "CityAdminId",
                principalTable: "CityAdminStaff",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthCareCenters_CityAdminStaff_CityAdminId",
                table: "HealthCareCenters");

            migrationBuilder.DropIndex(
                name: "IX_HealthCareCenters_CityAdminId",
                table: "HealthCareCenters");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e0efe84-8eef-4dae-a726-085ecc940340");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "608b0aa5-521f-4a29-86b7-91659cf99cce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acacc8ad-d2a5-44d0-83e0-989a7d0440d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e94a81b2-b746-4846-9fb0-4fc096b174d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f00bd245-6853-4e82-a671-f794fc10b752");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa10bbaf-a1ec-403d-aa53-41631193e50f");

            migrationBuilder.DropColumn(
                name: "CityAdminId",
                table: "HealthCareCenters");

            migrationBuilder.DropColumn(
                name: "IsResolved",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Chats");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chats",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a645924-052a-4646-b998-c7013ab50bcb", null, "organizer", "ORGANIZER" },
                    { "1bb3a8bd-a507-4db1-8e6e-a5bea6044d4c", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "2ea04679-3639-4b33-90cd-5be55dc13822", null, "doctor", "DOCTOR" },
                    { "4e741bc6-635b-43be-873e-32cdfec53bd9", null, "cityAdmin", "CITYADMIN" },
                    { "88dd76f5-7479-44db-b051-4f8152fb546c", null, "parent", "PARENT" },
                    { "ff0a1ceb-ced4-46b4-a4c3-17939ac4b027", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("2a7d506e-8bf2-4a18-8cd0-a9eee4c75890"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("6759c327-e243-43d9-a84b-32d847a4d267"), "بني مزار", "Sunday", "المنيا", "الوحده المحليه بقريه صفط ابوجرج", "Wednesday", "صفط ابوجرج" },
                    { new Guid("7fdc64ec-ebc4-498d-b3d1-8b30a73c1d01"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("8b2c98f2-ff92-4929-8927-7646b0138cd8"), "الجيزه", "Sunday", "الجيزه", "الوحده المحليه بالجيزه", "Wednesday", "منيل الروضه" },
                    { new Guid("94d7a785-f17e-4f98-a0ff-fc36d75fcd7d"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("bc49ce92-a4a2-45f7-98e0-74aedc967622"), "العدوي", "Sunday", "المنيا", "الوحده المحليه بالعدوي", "Wednesday", "العدوي" },
                    { new Guid("bd62e348-7184-4848-968c-fe11f6639ed0"), "عين شمس", "Sunday", "القاهره", "الوحده المحليه بالقاهره", "Tuesday", "عين شمس" }
                });
        }
    }
}
