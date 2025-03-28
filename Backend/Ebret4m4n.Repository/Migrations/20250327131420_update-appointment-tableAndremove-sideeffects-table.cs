using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateappointmenttableAndremovesideeffectstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Vaccines_VaccineId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "SideEffects");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_VaccineId",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d1d929d-90ff-4c2c-a16e-0b224699c735");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ec4f352-609a-4c8c-8a36-199ee6f199a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8f78845-f63b-4fb6-ba95-f249886419b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba605fa7-7ef7-4f2c-9b44-e3ee8a4705d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcdbdb1a-b276-40e6-9ef8-1bc77d928f05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d34c625e-5b69-4bff-ad28-c6fd17e3bf91");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("49c0c26c-5eb2-446a-9b16-6ebceecdde7d"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("4ae2ffa2-2674-4304-9e21-12da937f690d"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("6f9e69e0-f63a-4c51-9203-11a5539a9a2b"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("9c7202a1-4c14-4118-a947-b0cf58a7df75"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("a14d035e-13b7-495f-af2e-56b63d96853f"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("ce59bf53-03bf-412d-b5e3-f94e1138e069"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("ec2d43d0-1cc5-470d-9e10-3fa90add8d89"));

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "VaccineName",
                table: "Appointments",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "VaccineName",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "VaccineId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SideEffects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VaccineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SideEffects_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d1d929d-90ff-4c2c-a16e-0b224699c735", null, "admin", "ADMIN" },
                    { "6ec4f352-609a-4c8c-8a36-199ee6f199a9", null, "cityAdmin", "CITYADMIN" },
                    { "b8f78845-f63b-4fb6-ba95-f249886419b3", null, "doctor", "DOCTOR" },
                    { "ba605fa7-7ef7-4f2c-9b44-e3ee8a4705d4", null, "organizer", "ORGANIZER" },
                    { "bcdbdb1a-b276-40e6-9ef8-1bc77d928f05", null, "parent", "PARENT" },
                    { "d34c625e-5b69-4bff-ad28-c6fd17e3bf91", null, "governorateAdmin", "GOVERNORATEADMIN" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("49c0c26c-5eb2-446a-9b16-6ebceecdde7d"), "عين شمس", "Sunday", "القاهره", "الوحده المحليه بالقاهره", "Tuesday", "عين شمس" },
                    { new Guid("4ae2ffa2-2674-4304-9e21-12da937f690d"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("6f9e69e0-f63a-4c51-9203-11a5539a9a2b"), "بني مزار", "Sunday", "المنيا", "الوحده المحليه بقريه صفط ابوجرج", "Wednesday", "صفط ابوجرج" },
                    { new Guid("9c7202a1-4c14-4118-a947-b0cf58a7df75"), "الجيزه", "Sunday", "الجيزه", "الوحده المحليه بالجيزه", "Wednesday", "منيل الروضه" },
                    { new Guid("a14d035e-13b7-495f-af2e-56b63d96853f"), "العدوي", "Sunday", "المنيا", "الوحده المحليه بالعدوي", "Wednesday", "العدوي" },
                    { new Guid("ce59bf53-03bf-412d-b5e3-f94e1138e069"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("ec2d43d0-1cc5-470d-9e10-3fa90add8d89"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VaccineId",
                table: "Appointments",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_SideEffects_VaccineId",
                table: "SideEffects",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Vaccines_VaccineId",
                table: "Appointments",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
