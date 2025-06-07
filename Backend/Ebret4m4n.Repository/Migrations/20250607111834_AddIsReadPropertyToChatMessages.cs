using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadPropertyToChatMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ae3542-7439-40a6-a695-5d534eca803e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "678a0581-4aca-4b11-bd3b-43090f53c493");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb76f69f-7e1a-46e4-ab39-4827475241ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c232a9f6-725c-402d-a725-8959d9d2290c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc0d4e52-1d55-411b-b051-84b09feac46b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de5f4d85-3550-4bc7-9f2b-350a3076921d");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0aaebe30-54bd-4f6e-b80d-1bf2b29e4632", null, "organizer", "ORGANIZER" },
                    { "1da48dff-478c-427f-a218-cb895cfd790d", null, "doctor", "DOCTOR" },
                    { "3dc1e657-d94f-4fa6-a5d7-ec88e0a09f46", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "6a6c2b4c-d941-43d0-ba01-0afc8fc0dc10", null, "cityAdmin", "CITYADMIN" },
                    { "bf95cc9f-8dca-45f3-a050-23d93f494f93", null, "admin", "ADMIN" },
                    { "c8b8584e-3aa5-43ed-8db3-10a2d7dac736", null, "parent", "PARENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aaebe30-54bd-4f6e-b80d-1bf2b29e4632");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1da48dff-478c-427f-a218-cb895cfd790d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dc1e657-d94f-4fa6-a5d7-ec88e0a09f46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a6c2b4c-d941-43d0-ba01-0afc8fc0dc10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf95cc9f-8dca-45f3-a050-23d93f494f93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8b8584e-3aa5-43ed-8db3-10a2d7dac736");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Chats");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13ae3542-7439-40a6-a695-5d534eca803e", null, "admin", "ADMIN" },
                    { "678a0581-4aca-4b11-bd3b-43090f53c493", null, "doctor", "DOCTOR" },
                    { "bb76f69f-7e1a-46e4-ab39-4827475241ff", null, "parent", "PARENT" },
                    { "c232a9f6-725c-402d-a725-8959d9d2290c", null, "organizer", "ORGANIZER" },
                    { "dc0d4e52-1d55-411b-b051-84b09feac46b", null, "cityAdmin", "CITYADMIN" },
                    { "de5f4d85-3550-4bc7-9f2b-350a3076921d", null, "governorateAdmin", "GOVERNORATEADMIN" }
                });
        }
    }
}
