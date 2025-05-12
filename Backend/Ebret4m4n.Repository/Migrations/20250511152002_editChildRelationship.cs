using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editChildRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Children_ChildId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d239bea-9e8b-4da8-b2fa-3fa54a45b0bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10ff6608-b272-446f-8c7b-d78680dcfc65");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "addd976d-897f-4589-a270-4c8930aed1fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b25c187e-92be-4fd5-b410-587d5106f46b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba8a234d-a82f-4cad-babb-b35ead6aafb7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0cd504d-4242-40b4-a1a3-26b35c5693d8");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Children_ChildId",
                table: "Transactions",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Children_ChildId",
                table: "Transactions");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d239bea-9e8b-4da8-b2fa-3fa54a45b0bf", null, "cityAdmin", "CITYADMIN" },
                    { "10ff6608-b272-446f-8c7b-d78680dcfc65", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "addd976d-897f-4589-a270-4c8930aed1fa", null, "parent", "PARENT" },
                    { "b25c187e-92be-4fd5-b410-587d5106f46b", null, "doctor", "DOCTOR" },
                    { "ba8a234d-a82f-4cad-babb-b35ead6aafb7", null, "organizer", "ORGANIZER" },
                    { "e0cd504d-4242-40b4-a1a3-26b35c5693d8", null, "admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Children_ChildId",
                table: "Transactions",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id");
        }
    }
}
