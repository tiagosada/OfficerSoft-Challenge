using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class cpfMaskFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0ea86ba-fb2c-485e-859c-727e61b6ef7e"));

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "People",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("81736a16-bf3f-41cb-90e3-d11a3e9fc4b3"), "0192023A7BBD73250516F069DF18B500", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("81736a16-bf3f-41cb-90e3-d11a3e9fc4b3"));

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("b0ea86ba-fb2c-485e-859c-727e61b6ef7e"), "0192023A7BBD73250516F069DF18B500", "Admin" });
        }
    }
}
