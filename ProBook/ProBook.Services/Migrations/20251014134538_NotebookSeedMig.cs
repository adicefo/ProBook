using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class NotebookSeedMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Notebooks",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-1", 1 },
                    { 2, new DateTime(2025, 10, 13, 20, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-2", 1 },
                    { 3, new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-3", 1 },
                    { 4, new DateTime(2025, 10, 13, 21, 49, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-1", 1 },
                    { 5, new DateTime(2025, 10, 13, 21, 0, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-2", 1 },
                    { 6, new DateTime(2025, 10, 14, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-1", 2 },
                    { 7, new DateTime(2025, 10, 14, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-2", 2 },
                    { 8, new DateTime(2025, 10, 14, 19, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-3", 2 },
                    { 9, new DateTime(2025, 10, 14, 11, 49, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-1", 2 },
                    { 10, new DateTime(2025, 10, 14, 23, 49, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-2", 2 },
                    { 11, new DateTime(2025, 10, 14, 22, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-1", 3 },
                    { 12, new DateTime(2025, 10, 14, 22, 49, 5, 0, DateTimeKind.Utc), "-", null, "Math-2", 3 },
                    { 13, new DateTime(2025, 9, 13, 19, 11, 5, 0, DateTimeKind.Utc), "-", null, "Math-3", 3 },
                    { 14, new DateTime(2025, 10, 14, 19, 55, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-1", 3 },
                    { 15, new DateTime(2025, 10, 14, 19, 24, 5, 0, DateTimeKind.Utc), "-", null, "Bosnian-2", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Notebooks",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
