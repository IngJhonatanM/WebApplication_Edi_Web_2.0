using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication_Edi_Web_2._0.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68758999-8cab-4262-8497-f65fa03d81c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fbb0518-abcf-4563-981e-240d497564b2");

            migrationBuilder.AddColumn<string>(
                name: "DescripUser",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b7e08332-f87e-4e91-a417-d0881f4cf2da", null, "User", "User" },
                    { "f80917db-08d0-4b5a-9d38-62d5533e60a6", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7e08332-f87e-4e91-a417-d0881f4cf2da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f80917db-08d0-4b5a-9d38-62d5533e60a6");

            migrationBuilder.DropColumn(
                name: "DescripUser",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "68758999-8cab-4262-8497-f65fa03d81c3", null, "User", "User" },
                    { "6fbb0518-abcf-4563-981e-240d497564b2", null, "Admin", "Admin" }
                });
        }
    }
}
