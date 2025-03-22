using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class modifyingChattableAndNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_UserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_MedicalStaff_MedicalStaffId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_MedicalStaffId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

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
                name: "MedicalStaffId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Certificates",
                newName: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRequested",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecievedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GovernorateAdminId",
                table: "CityAdminStaff",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentAt",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.CreateIndex(
                name: "IX_CityAdminStaff_GovernorateAdminId",
                table: "CityAdminStaff",
                column: "GovernorateAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chats",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SenderId",
                table: "Chats",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_ReceiverId",
                table: "Chats",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_SenderId",
                table: "Chats",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityAdminStaff_GovernorateAdminStaff_GovernorateAdminId",
                table: "CityAdminStaff",
                column: "GovernorateAdminId",
                principalTable: "GovernorateAdminStaff",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_ReceiverId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_SenderId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_CityAdminStaff_GovernorateAdminStaff_GovernorateAdminId",
                table: "CityAdminStaff");

            migrationBuilder.DropIndex(
                name: "IX_CityAdminStaff_GovernorateAdminId",
                table: "CityAdminStaff");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_SenderId",
                table: "Chats");

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
                name: "DateRequested",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "RecievedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "GovernorateAdminId",
                table: "CityAdminStaff");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Certificates",
                newName: "date");

            migrationBuilder.AddColumn<string>(
                name: "MedicalStaffId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
                name: "IX_Chats_MedicalStaffId",
                table: "Chats",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_MedicalStaff_MedicalStaffId",
                table: "Chats",
                column: "MedicalStaffId",
                principalTable: "MedicalStaff",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
