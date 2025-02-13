using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class SecondEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "070d6357-d3d4-4e31-9313-1b6096e236f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13c1eb68-227a-4cc8-8be7-b40f94bca1fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cb19600-1c86-4fda-b4e9-4f8704148e03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "873eef10-6802-4870-a1de-f206c2aa1258");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_VaccineId",
                table: "Inventories",
                newName: "IX_Inventories_VaccineId");

            migrationBuilder.AddColumn<string>(
                name: "FirstDay",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondDay",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstDay",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondDay",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "608d6f8e-cbcd-433f-8bc7-1bbbb64eb960", null, "Parent", null },
                    { "6ab71809-462f-4c1e-b416-51155189308a", null, "AdminOfMinistryOfHealth", null },
                    { "c17e4689-32bf-40d1-ac35-133b8a96eadb", null, "AdminOfHC", null },
                    { "c6323d02-c868-422c-8f50-cb1f71230c99", null, "Doctor", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_HealthCareCenterId",
                table: "Inventories",
                column: "HealthCareCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventory_HealthCareCenterId",
                table: "Inventories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "608d6f8e-cbcd-433f-8bc7-1bbbb64eb960");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ab71809-462f-4c1e-b416-51155189308a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c17e4689-32bf-40d1-ac35-133b8a96eadb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6323d02-c868-422c-8f50-cb1f71230c99");

            migrationBuilder.DropColumn(
                name: "FirstDay",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "SecondDay",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "FirstDay",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "SecondDay",
                table: "Doctor");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_VaccineId",
                table: "Inventories",
                newName: "IX_Inventory_VaccineId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "070d6357-d3d4-4e31-9313-1b6096e236f0", null, "Parent", null },
                    { "13c1eb68-227a-4cc8-8be7-b40f94bca1fa", null, "AdminOfMinistryOfHealth", null },
                    { "3cb19600-1c86-4fda-b4e9-4f8704148e03", null, "Doctor", null },
                    { "873eef10-6802-4870-a1de-f206c2aa1258", null, "AdminOfHC", null }
                });
        }
    }
}
