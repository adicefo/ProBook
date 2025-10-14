using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class NotebookCollectionSeedMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotebookCollections",
                columns: new[] { "Id", "CollectionId", "CreatedAt", "NotebookId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 11, 19, 49, 5, 0, DateTimeKind.Utc), 1 },
                    { 2, 1, new DateTime(2025, 10, 11, 11, 49, 5, 0, DateTimeKind.Utc), 2 },
                    { 3, 1, new DateTime(2025, 10, 11, 10, 49, 5, 0, DateTimeKind.Utc), 3 },
                    { 4, 2, new DateTime(2025, 10, 10, 19, 49, 5, 0, DateTimeKind.Utc), 4 },
                    { 5, 2, new DateTime(2025, 10, 9, 19, 49, 5, 0, DateTimeKind.Utc), 5 },
                    { 6, 3, new DateTime(2025, 10, 9, 12, 49, 5, 0, DateTimeKind.Utc), 6 },
                    { 7, 3, new DateTime(2025, 10, 6, 19, 49, 5, 0, DateTimeKind.Utc), 7 },
                    { 8, 3, new DateTime(2025, 10, 5, 19, 49, 5, 0, DateTimeKind.Utc), 8 },
                    { 9, 4, new DateTime(2025, 10, 11, 19, 49, 5, 0, DateTimeKind.Utc), 9 },
                    { 10, 4, new DateTime(2025, 10, 1, 19, 49, 5, 0, DateTimeKind.Utc), 10 },
                    { 11, 5, new DateTime(2025, 10, 2, 19, 49, 5, 0, DateTimeKind.Utc), 11 },
                    { 12, 5, new DateTime(2025, 10, 4, 22, 49, 5, 0, DateTimeKind.Utc), 12 },
                    { 13, 5, new DateTime(2025, 6, 11, 19, 49, 5, 0, DateTimeKind.Utc), 13 },
                    { 14, 6, new DateTime(2025, 9, 11, 19, 49, 5, 0, DateTimeKind.Utc), 14 },
                    { 15, 6, new DateTime(2025, 10, 6, 19, 49, 5, 0, DateTimeKind.Utc), 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "NotebookCollections",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
