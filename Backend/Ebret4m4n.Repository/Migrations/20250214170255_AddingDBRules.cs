using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ebret4m4n.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddingDBRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_Children_ChildId",
                table: "Vaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthReportFiles_Children_ChildId",
                table: "HealthReportFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Diseas_Children_ChildId",
                table: "Diseas");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Children_ChildId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Children_ChildId",
                table: "Appointments");

            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Children",
                table: "Children");

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
                name: "IsDefult",
                table: "Vaccines");

            migrationBuilder.RenameIndex(
                name: "IX_Vacccine_Name",
                table: "Vaccines",
                newName: "IX_Vaccines_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_HealthCareCenterId",
                table: "Inventories",
                newName: "IX_Inventories_HealthCareCenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_UserId",
                table: "Complaints",
                newName: "IX_Complaints_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Child_Name",
                table: "Children",
                newName: "IX_Children_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_ChildId",
                table: "Certificates",
                newName: "IX_Certificates_ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_UserId",
                table: "Appointments",
                newName: "IX_Appointments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_ChildId",
                table: "Appointments",
                newName: "IX_Appointments_ChildId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vaccines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsTaken",
                table: "Vaccines",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            // Alter the ChildId column in the Vaccines table
            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "Vaccines",
                type: "nvarchar(14)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Alter the ChildId column in the HealthReportFiles table
            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "HealthReportFiles",
                type: "nvarchar(14)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Alter the ChildId column in the Diseas table
            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "Diseas",
                type: "nvarchar(14)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Alter the ChildId column in the Certificates table
            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "Certificates",
                type: "nvarchar(14)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Alter the ChildId column in the Appointments table
            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "Appointments",
                type: "nvarchar(14)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Alter the Id column in the Children table
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Children",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Recreate the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Children",
                table: "Children",
                column: "Id");

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_Children_ChildId",
                table: "Vaccines",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthReportFiles_Children_ChildId",
                table: "HealthReportFiles",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diseas_Children_ChildId",
                table: "Diseas",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Children_ChildId",
                table: "Certificates",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //NoAction
            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Children_ChildId",
                table: "Appointments",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SideEffects",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HealthCareCenterName",
                table: "Inventories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HealthCareCenterName",
                table: "HealthCareCenters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HealthCareCenterName",
                table: "Doctor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Severity",
                table: "Diseas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Diseas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Diseas",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Complaints",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Children",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chats",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Appointments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AgeInMonth",
                table: "Children",
                type: "int",
                nullable: false,
                computedColumnSql: "CAST(DATEDIFF(DAY, BirthDate, GETDATE()) / 30.0 AS INT)",
                stored: false);

            migrationBuilder.CreateTable(
                name: "AdminOfHC",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NursingNumber = table.Column<int>(type: "int", nullable: false),
                    HealthCareCenterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HealthCareLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2510c840-b28a-43c2-b825-9f179ac2d159", null, "AdminOfMinistryOfHealth", null },
                    { "81b0cc65-49f5-4b89-8831-e9542760ce09", null, "Parent", null },
                    { "867a7f12-f934-4772-8232-b0696c431e88", null, "Doctor", null },
                    { "e775ae18-9334-4f02-babe-26eb94209c36", null, "AdminOfHC", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthCareCenters_HealthCareCenterName",
                table: "HealthCareCenters",
                column: "HealthCareCenterName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminOfHC");

            migrationBuilder.DropIndex(
                name: "IX_HealthCareCenters_HealthCareCenterName",
                table: "HealthCareCenters");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2510c840-b28a-43c2-b825-9f179ac2d159");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81b0cc65-49f5-4b89-8831-e9542760ce09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "867a7f12-f934-4772-8232-b0696c431e88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e775ae18-9334-4f02-babe-26eb94209c36");

            migrationBuilder.DropColumn(
                name: "AgeInMonth",
                table: "Children");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccines_Name",
                table: "Vaccines",
                newName: "IX_Vacccine_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_HealthCareCenterId",
                table: "Inventories",
                newName: "IX_Inventory_HealthCareCenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints",
                newName: "IX_Complaint_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Children_Name",
                table: "Children",
                newName: "IX_Child_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_ChildId",
                table: "Certificates",
                newName: "IX_Certificate_ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                newName: "IX_Appointment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ChildId",
                table: "Appointments",
                newName: "IX_Appointment_ChildId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vaccines",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsTaken",
                table: "Vaccines",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChildId",
                table: "Vaccines",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefult",
                table: "Vaccines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SideEffects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350);

            migrationBuilder.AlterColumn<string>(
                name: "HealthCareCenterName",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChildId",
                table: "HealthReportFiles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)");

            migrationBuilder.AlterColumn<string>(
                name: "HealthCareCenterName",
                table: "HealthCareCenters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "HealthCareCenterName",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Severity",
                table: "Diseas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Diseas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Diseas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChildId",
                table: "Diseas",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Children",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Children",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChildId",
                table: "Certificates",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChildId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)");

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
        }
    }
}