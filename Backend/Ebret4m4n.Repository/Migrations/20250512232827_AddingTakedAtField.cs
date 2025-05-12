using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddingTakedAtField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fa96afd-744d-47fc-b385-445be31e1754");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52c3f2a1-b076-4369-921b-dbde2acc1abc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a14d5168-b3b6-4c27-ad4c-f8d43ef2775b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5069509-db22-4904-b2a9-29e9abc31e64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b44675bf-b2f3-404a-b546-73000d75b599");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb5b2245-eecd-447a-bba4-a69f4b401b26");

            migrationBuilder.AddColumn<DateTime>(
                name: "TakedAt",
                table: "Vaccines",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TakedAt",
                table: "Vaccines");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fa96afd-744d-47fc-b385-445be31e1754", null, "organizer", "ORGANIZER" },
                    { "52c3f2a1-b076-4369-921b-dbde2acc1abc", null, "doctor", "DOCTOR" },
                    { "a14d5168-b3b6-4c27-ad4c-f8d43ef2775b", null, "parent", "PARENT" },
                    { "a5069509-db22-4904-b2a9-29e9abc31e64", null, "cityAdmin", "CITYADMIN" },
                    { "b44675bf-b2f3-404a-b546-73000d75b599", null, "admin", "ADMIN" },
                    { "cb5b2245-eecd-447a-bba4-a69f4b401b26", null, "governorateAdmin", "GOVERNORATEADMIN" }
                });
        }
    }
}
