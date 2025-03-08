using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class modifyTheInvenrotyTableAndAddOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_Inventories_InventoryId",
                table: "Vaccines");

            migrationBuilder.DropTable(
                name: "MedicalApplications");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_InventoryId",
                table: "Vaccines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_HealthCareCenterId",
                table: "Inventories");

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

            migrationBuilder.DropColumn(
                name: "DocesRequired",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "DocesTaken",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "MedicalNumber",
                table: "MedicalStaff");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "FirstDay",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterCity",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterGovernorate",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterName",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterVillage",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "SecondDay",
                table: "Inventories");

            migrationBuilder.AddColumn<string>(
                name: "Antigen",
                table: "Inventories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "Complaints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsNoramal",
                table: "Children",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                columns: new[] { "HealthCareCenterId", "Antigen" });

            migrationBuilder.CreateTable(
                name: "MainInventory",
                columns: table => new
                {
                    InventoryLocation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Antigen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    CityAdminStaffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GovernorateAdminStaffId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainInventory", x => new { x.InventoryLocation, x.Antigen });
                    table.ForeignKey(
                        name: "FK_MainInventory_CityAdminStaff_CityAdminStaffId",
                        column: x => x.CityAdminStaffId,
                        principalTable: "CityAdminStaff",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_MainInventory_GovernorateAdminStaff_GovernorateAdminStaffId",
                        column: x => x.GovernorateAdminStaffId,
                        principalTable: "GovernorateAdminStaff",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Antigen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalStaffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CityAdminStaffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GovernorateAdminStaffId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_CityAdminStaff_CityAdminStaffId",
                        column: x => x.CityAdminStaffId,
                        principalTable: "CityAdminStaff",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Orders_GovernorateAdminStaff_GovernorateAdminStaffId",
                        column: x => x.GovernorateAdminStaffId,
                        principalTable: "GovernorateAdminStaff",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Orders_MedicalStaff_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaff",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1207aba1-90b0-41b1-b693-ac35e1f5b5e9", null, "organizer", "ORGANIZER" },
                    { "18b49dbf-8208-4faa-8c4f-26f13eae874a", null, "doctor", "DOCTOR" },
                    { "19fb137e-d72f-4a88-a218-b1c0fd07d966", null, "cityAdmin", "CITYADMIN" },
                    { "32713898-08ac-48eb-bf84-3611bcfe5677", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "a1ee9400-805c-4bb8-80fa-10ad1d146fb8", null, "parent", "PARENT" },
                    { "d1f4b38e-dc2b-424c-a815-480bd796cbc1", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("249c9384-3e76-4729-8cb9-8840a5f6c191"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("71230496-1302-45c5-9487-f60863f1907c"), "العدوي", "Sunday", "المنيا", "الوحده المحليه بالعدوي", "Wednesday", "العدوي" },
                    { new Guid("8351d9b4-1c0d-4a1c-b253-3f7cc78d17e9"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("9c69ca87-1910-4f23-9b54-37c8d440ef78"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" },
                    { new Guid("af21e432-161e-419f-9b11-be8d4d8c0cb3"), "عين شمس", "Sunday", "القاهره", "الوحده المحليه بالقاهره", "Tuesday", "عين شمس" },
                    { new Guid("dc492686-6a33-428e-bdba-c2524ac7e44a"), "بني مزار", "Sunday", "المنيا", "الوحده المحليه بقريه صفط ابوجرج", "Wednesday", "صفط ابوجرج" },
                    { new Guid("e3778f08-e87e-4389-8ea4-4a73c004288a"), "الجيزه", "Sunday", "الجيزه", "الوحده المحليه بالجيزه", "Wednesday", "منيل الروضه" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainInventory_CityAdminStaffId",
                table: "MainInventory",
                column: "CityAdminStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_MainInventory_GovernorateAdminStaffId",
                table: "MainInventory",
                column: "GovernorateAdminStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CityAdminStaffId",
                table: "Orders",
                column: "CityAdminStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GovernorateAdminStaffId",
                table: "Orders",
                column: "GovernorateAdminStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MedicalStaffId",
                table: "Orders",
                column: "MedicalStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers",
                column: "HealthCareCenterId",
                principalTable: "HealthCareCenters",
                principalColumn: "HealthCareCenterId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_HealthCareCenters_HealthCareCenterId",
                table: "Inventories",
                column: "HealthCareCenterId",
                principalTable: "HealthCareCenters",
                principalColumn: "HealthCareCenterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_HealthCareCenters_HealthCareCenterId",
                table: "Inventories");

            migrationBuilder.DropTable(
                name: "MainInventory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1207aba1-90b0-41b1-b693-ac35e1f5b5e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18b49dbf-8208-4faa-8c4f-26f13eae874a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19fb137e-d72f-4a88-a218-b1c0fd07d966");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32713898-08ac-48eb-bf84-3611bcfe5677");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1ee9400-805c-4bb8-80fa-10ad1d146fb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1f4b38e-dc2b-424c-a815-480bd796cbc1");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("249c9384-3e76-4729-8cb9-8840a5f6c191"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("71230496-1302-45c5-9487-f60863f1907c"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("8351d9b4-1c0d-4a1c-b253-3f7cc78d17e9"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("9c69ca87-1910-4f23-9b54-37c8d440ef78"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("af21e432-161e-419f-9b11-be8d4d8c0cb3"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("dc492686-6a33-428e-bdba-c2524ac7e44a"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("e3778f08-e87e-4389-8ea4-4a73c004288a"));

            migrationBuilder.DropColumn(
                name: "Antigen",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "IsNoramal",
                table: "Children");

            migrationBuilder.AddColumn<int>(
                name: "DocesRequired",
                table: "Vaccines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocesTaken",
                table: "Vaccines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "InventoryId",
                table: "Vaccines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalNumber",
                table: "MedicalStaff",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Inventories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FirstDay",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterCity",
                table: "Inventories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterGovernorate",
                table: "Inventories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterName",
                table: "Inventories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterVillage",
                table: "Inventories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondDay",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MedicalApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicantPosition = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HealthCareCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthCareGovernorate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthCareName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthCareVillage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalApplications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_MedicalApplications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_InventoryId",
                table: "Vaccines",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_HealthCareCenterId",
                table: "Inventories",
                column: "HealthCareCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalApplications_UserId",
                table: "MedicalApplications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers",
                column: "HealthCareCenterId",
                principalTable: "HealthCareCenters",
                principalColumn: "HealthCareCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_Inventories_InventoryId",
                table: "Vaccines",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id");
        }
    }
}
