using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class NewMig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Gender", "IsStudent", "Name", "PasswordHash", "PasswordSalt", "RegisteredDate", "Surname", "TelephoneNumber", "Username" },
                values: new object[,]
                {
                    { 1, "user1@gmail.com", "Male", true, "User1", "UbzzxOGag4pPmBhguTkyKnpEZw4=", "qYk4OxryQgplthbzFlS0yQ==", new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "User1", "061-234-444", "user1" },
                    { 2, "user2@gmail.com", "Male", true, "User2", "UbzzxOGag4pPmBhguTkyKnpEZw4=", "qYk4OxryQgplthbzFlS0yQ==", new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "User2", "063-234-444", "user2" },
                    { 3, "user3@gmail.com", "Female", true, "User3", "UbzzxOGag4pPmBhguTkyKnpEZw4=", "qYk4OxryQgplthbzFlS0yQ==", new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "User3", "065-234-444", "user3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
