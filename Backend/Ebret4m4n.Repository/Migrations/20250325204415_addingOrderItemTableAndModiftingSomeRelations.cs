using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addingOrderItemTableAndModiftingSomeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "162c723a-220d-4c0f-b1e9-108f4ca51593");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65b0991c-ba13-4df7-a6c4-494bfc9f5b15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69a35668-ac5a-435f-a8f3-345d8085b960");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b11e33c2-21ce-4f1b-bd79-c2eb4842c43c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3d22860-0095-445c-b7c4-01287deb1170");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdf25cf2-cd8d-4601-9b40-da278427135e");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("11f66134-aa05-4b17-8aa7-06065c6e1b90"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("5a88b5dc-2353-41d8-9ec1-57ea42f70cfe"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("773fbd65-6e90-4699-b811-ff7b374144ea"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("9d604a2c-ae6f-420c-a58a-333d5b959709"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("ab18bc95-d83c-4a26-8971-4bc2fad39deb"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("ac6fe554-db7e-4c93-b9bc-2ced83ef8618"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("bb79b47e-3457-45ed-bd0d-c0ac43d5e7e7"));

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Antigen",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityAdminStaffId",
                table: "MedicalStaff",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CityAdminStaffUserId",
                table: "MedicalStaff",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Antigen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
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
                name: "IX_MedicalStaff_CityAdminStaffId",
                table: "MedicalStaff",
                column: "CityAdminStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaff_CityAdminStaffUserId",
                table: "MedicalStaff",
                column: "CityAdminStaffUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStaff_CityAdminStaff_CityAdminStaffId",
                table: "MedicalStaff",
                column: "CityAdminStaffId",
                principalTable: "CityAdminStaff",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStaff_CityAdminStaff_CityAdminStaffUserId",
                table: "MedicalStaff",
                column: "CityAdminStaffUserId",
                principalTable: "CityAdminStaff",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStaff_CityAdminStaff_CityAdminStaffId",
                table: "MedicalStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStaff_CityAdminStaff_CityAdminStaffUserId",
                table: "MedicalStaff");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_MedicalStaff_CityAdminStaffId",
                table: "MedicalStaff");

            migrationBuilder.DropIndex(
                name: "IX_MedicalStaff_CityAdminStaffUserId",
                table: "MedicalStaff");

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
                name: "DateApproved",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CityAdminStaffId",
                table: "MedicalStaff");

            migrationBuilder.DropColumn(
                name: "CityAdminStaffUserId",
                table: "MedicalStaff");

            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Antigen",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "162c723a-220d-4c0f-b1e9-108f4ca51593", null, "doctor", "DOCTOR" },
                    { "65b0991c-ba13-4df7-a6c4-494bfc9f5b15", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "69a35668-ac5a-435f-a8f3-345d8085b960", null, "cityAdmin", "CITYADMIN" },
                    { "b11e33c2-21ce-4f1b-bd79-c2eb4842c43c", null, "organizer", "ORGANIZER" },
                    { "b3d22860-0095-445c-b7c4-01287deb1170", null, "parent", "PARENT" },
                    { "cdf25cf2-cd8d-4601-9b40-da278427135e", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("11f66134-aa05-4b17-8aa7-06065c6e1b90"), "عين شمس", "Sunday", "القاهره", "الوحده المحليه بالقاهره", "Tuesday", "عين شمس" },
                    { new Guid("5a88b5dc-2353-41d8-9ec1-57ea42f70cfe"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("773fbd65-6e90-4699-b811-ff7b374144ea"), "بني مزار", "Sunday", "المنيا", "الوحده المحليه بقريه صفط ابوجرج", "Wednesday", "صفط ابوجرج" },
                    { new Guid("9d604a2c-ae6f-420c-a58a-333d5b959709"), "العدوي", "Sunday", "المنيا", "الوحده المحليه بالعدوي", "Wednesday", "العدوي" },
                    { new Guid("ab18bc95-d83c-4a26-8971-4bc2fad39deb"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("ac6fe554-db7e-4c93-b9bc-2ced83ef8618"), "الجيزه", "Sunday", "الجيزه", "الوحده المحليه بالجيزه", "Wednesday", "منيل الروضه" },
                    { new Guid("bb79b47e-3457-45ed-bd0d-c0ac43d5e7e7"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" }
                });
        }
    }
}
