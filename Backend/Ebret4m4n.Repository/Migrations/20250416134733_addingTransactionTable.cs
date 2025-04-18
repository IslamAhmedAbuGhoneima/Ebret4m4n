using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addingTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e0efe84-8eef-4dae-a726-085ecc940340");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "608b0aa5-521f-4a29-86b7-91659cf99cce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acacc8ad-d2a5-44d0-83e0-989a7d0440d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e94a81b2-b746-4846-9fb0-4fc096b174d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f00bd245-6853-4e82-a671-f794fc10b752");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa10bbaf-a1ec-403d-aa53-41631193e50f");

            migrationBuilder.RenameColumn(
                name: "IsNoramal",
                table: "Children",
                newName: "IsNormal");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ChildId = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: true),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ChildId",
                table: "Transactions",
                column: "ChildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SessionId",
                table: "Transactions",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

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

            migrationBuilder.RenameColumn(
                name: "IsNormal",
                table: "Children",
                newName: "IsNoramal");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e0efe84-8eef-4dae-a726-085ecc940340", null, "parent", "PARENT" },
                    { "608b0aa5-521f-4a29-86b7-91659cf99cce", null, "governorateAdmin", "GOVERNORATEADMIN" },
                    { "acacc8ad-d2a5-44d0-83e0-989a7d0440d5", null, "admin", "ADMIN" },
                    { "e94a81b2-b746-4846-9fb0-4fc096b174d2", null, "cityAdmin", "CITYADMIN" },
                    { "f00bd245-6853-4e82-a671-f794fc10b752", null, "doctor", "DOCTOR" },
                    { "fa10bbaf-a1ec-403d-aa53-41631193e50f", null, "organizer", "ORGANIZER" }
                });
        }
    }
}
