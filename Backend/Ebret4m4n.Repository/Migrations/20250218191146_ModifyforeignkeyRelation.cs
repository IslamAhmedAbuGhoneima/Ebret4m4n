using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ModifyforeignkeyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3645457-7df9-46b5-acf3-5d6e3095a986");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eea5447a-7889-4458-b6ac-92c5ba404809");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc80e67a-2bb3-485b-b6e8-7857ebecc9d5");

            migrationBuilder.DropColumn(
                name: "HealthCareLocation",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HealthCareLocation",
                table: "AdminOfHC");

            migrationBuilder.AlterColumn<Guid>(
                name: "HealthCareCenterId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "080ae79d-234b-4705-893c-c39f7372e0d0", null, "AdminOfHC", null },
                    { "1ae13225-48c0-4588-98ee-99538cf08d16", null, "Doctor", null },
                    { "d96aef87-1fad-423c-9dc4-5b81440d4fb7", null, "AdminOfMinistryOfHealth", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers",
                column: "HealthCareCenterId",
                principalTable: "HealthCareCenters",
                principalColumn: "HealthCareCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "080ae79d-234b-4705-893c-c39f7372e0d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ae13225-48c0-4588-98ee-99538cf08d16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d96aef87-1fad-423c-9dc4-5b81440d4fb7");

            migrationBuilder.AddColumn<string>(
                name: "HealthCareLocation",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "HealthCareCenterId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthCareLocation",
                table: "AdminOfHC",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d3645457-7df9-46b5-acf3-5d6e3095a986", null, "AdminOfMinistryOfHealth", null },
                    { "eea5447a-7889-4458-b6ac-92c5ba404809", null, "Doctor", null },
                    { "fc80e67a-2bb3-485b-b6e8-7857ebecc9d5", null, "AdminOfHC", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HealthCareCenters_HealthCareCenterId",
                table: "AspNetUsers",
                column: "HealthCareCenterId",
                principalTable: "HealthCareCenters",
                principalColumn: "HealthCareCenterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
