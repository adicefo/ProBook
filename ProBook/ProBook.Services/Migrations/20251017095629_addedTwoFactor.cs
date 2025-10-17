using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class addedTwoFactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorAuthEnabled",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoFactorCode",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TwoFactorAuthEnabled", "TwoFactorCode" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TwoFactorAuthEnabled", "TwoFactorCode" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TwoFactorAuthEnabled", "TwoFactorCode" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorAuthEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwoFactorCode",
                table: "Users");
        }
    }
}
