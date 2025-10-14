using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class CollectionSeedMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 14, 22, 49, 5, 0, DateTimeKind.Utc), "-", "Math-Collection", 1 },
                    { 2, new DateTime(2025, 10, 14, 21, 49, 5, 0, DateTimeKind.Utc), "-", "Bosnian-Collection", 1 },
                    { 3, new DateTime(2025, 10, 14, 11, 49, 5, 0, DateTimeKind.Utc), "-", "Math-Collection", 2 },
                    { 4, new DateTime(2025, 10, 14, 13, 49, 5, 0, DateTimeKind.Utc), "-", "Bosnian-Collection", 2 },
                    { 5, new DateTime(2025, 10, 13, 11, 49, 5, 0, DateTimeKind.Utc), "-", "Math-Collection", 3 },
                    { 6, new DateTime(2025, 10, 13, 22, 49, 5, 0, DateTimeKind.Utc), "-", "Bosnian-Collection", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
