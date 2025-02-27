using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ModyfingApplicationUserTableAndAddMedicalStaffTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Doctor_DoctorId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "AdminOfHC");

            migrationBuilder.DropTable(
                name: "Doctor");

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

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Chats",
                newName: "MedicalStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_DoctorId",
                table: "Chats",
                newName: "IX_Chats_MedicalStaffId");

            migrationBuilder.CreateTable(
                name: "MedicalStaff",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicalNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    HealthCareCenterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HealthCareCenterGovernment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HealthCareCenterCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    HealthCareCenterVillage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    StaffRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalStaff", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_MedicalStaff_AspNetUsers_UserId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_MedicalStaff_MedicalStaffId",
                table: "Chats",
                column: "MedicalStaffId",
                principalTable: "MedicalStaff",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_MedicalStaff_MedicalStaffId",
                table: "Chats");

            migrationBuilder.DropTable(
                name: "MedicalStaff");

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

            migrationBuilder.RenameColumn(
                name: "MedicalStaffId",
                table: "Chats",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_MedicalStaffId",
                table: "Chats",
                newName: "IX_Chats_DoctorId");

            migrationBuilder.CreateTable(
                name: "AdminOfHC",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HealthCareCenterCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    HealthCareCenterGovernment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HealthCareCenterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HealthCareCenterVillage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MedicalNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SecondDay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminOfHC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminOfHC_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HealthCareCenterCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    HealthCareCenterGovernment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HealthCareCenterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HealthCareCenterVillage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MedicalNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SecondDay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Doctor_DoctorId",
                table: "Chats",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
