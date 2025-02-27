using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddingApplicationTableWithSomeModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32a517a5-ccc8-4ebb-b7df-3a4569e38924");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6880f1c8-a193-4e05-8688-7c907a899ec4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5eefc6b-beb0-4c39-a9a5-e1c805738a46");

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("017de2f1-8389-438b-83a8-5b92ae4368ff"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("0e5d0618-f1d6-433e-b71e-ab3b3914e61f"));

            migrationBuilder.DeleteData(
                table: "HealthCareCenters",
                keyColumn: "HealthCareCenterId",
                keyValue: new Guid("e9431bdb-396b-4a0b-a1fa-0e6c4ed98ba3"));

            migrationBuilder.DropColumn(
                name: "DoctorNumber",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "NursingNumber",
                table: "AdminOfHC");

            migrationBuilder.AddColumn<Guid>(
                name: "HCCenterId",
                table: "Doctor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalNumber",
                table: "Doctor",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "HCCenterId",
                table: "AdminOfHC",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalNumber",
                table: "AdminOfHC",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicalNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_JobApplications_AspNetUsers_UserId",
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

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_UserId",
                table: "JobApplications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");

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

            migrationBuilder.DropColumn(
                name: "HCCenterId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "MedicalNumber",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HCCenterId",
                table: "AdminOfHC");

            migrationBuilder.DropColumn(
                name: "MedicalNumber",
                table: "AdminOfHC");

            migrationBuilder.AddColumn<int>(
                name: "DoctorNumber",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NursingNumber",
                table: "AdminOfHC",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32a517a5-ccc8-4ebb-b7df-3a4569e38924", null, "AdminOfMinistryOfHealth", null },
                    { "6880f1c8-a193-4e05-8688-7c907a899ec4", null, "Doctor", null },
                    { "b5eefc6b-beb0-4c39-a9a5-e1c805738a46", null, "AdminOfHC", null }
                });

            migrationBuilder.InsertData(
                table: "HealthCareCenters",
                columns: new[] { "HealthCareCenterId", "City", "FirstDay", "Governorate", "HealthCareCenterName", "SecondDay", "Village" },
                values: new object[,]
                {
                    { new Guid("017de2f1-8389-438b-83a8-5b92ae4368ff"), "بني مزار", "Monday", "المنيا", "الوحده المحليه بقريه ابوجرج", "Tuesday", "ابوجرج" },
                    { new Guid("0e5d0618-f1d6-433e-b71e-ab3b3914e61f"), "المنيا", "Saturday", "المنيا", "الوحده المحليه بقريه البرجايه", "Tuesday", "البرجايه" },
                    { new Guid("e9431bdb-396b-4a0b-a1fa-0e6c4ed98ba3"), "مغاغا", "Sunday", "المنيا", "الوحده المحليه بقريه دهمرو", "Wednesday", "دهمرو" }
                });
        }
    }
}
