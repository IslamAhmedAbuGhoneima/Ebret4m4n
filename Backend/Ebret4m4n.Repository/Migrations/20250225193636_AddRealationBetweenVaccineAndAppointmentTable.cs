using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddRealationBetweenVaccineAndAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Village",
                table: "Inventories",
                newName: "HealthCareCenterVillage");

            migrationBuilder.RenameColumn(
                name: "Governorate",
                table: "Inventories",
                newName: "HealthCareCenterGovernment");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Inventories",
                newName: "HealthCareCenterCity");

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterCity",
                table: "Doctor",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterGovernment",
                table: "Doctor",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterVillage",
                table: "Doctor",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VaccineId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterCity",
                table: "AdminOfHC",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterGovernment",
                table: "AdminOfHC",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HealthCareCenterVillage",
                table: "AdminOfHC",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VaccineId",
                table: "Appointments",
                column: "VaccineId");

            // update onDelete to NoAction
            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Vaccines_VaccineId",
                table: "Appointments",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Vaccines_VaccineId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_VaccineId",
                table: "Appointments");

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
                name: "HealthCareCenterCity",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterGovernment",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterVillage",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterCity",
                table: "AdminOfHC");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterGovernment",
                table: "AdminOfHC");

            migrationBuilder.DropColumn(
                name: "HealthCareCenterVillage",
                table: "AdminOfHC");

            migrationBuilder.RenameColumn(
                name: "HealthCareCenterVillage",
                table: "Inventories",
                newName: "Village");

            migrationBuilder.RenameColumn(
                name: "HealthCareCenterGovernment",
                table: "Inventories",
                newName: "Governorate");

            migrationBuilder.RenameColumn(
                name: "HealthCareCenterCity",
                table: "Inventories",
                newName: "City");

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
    }
}
