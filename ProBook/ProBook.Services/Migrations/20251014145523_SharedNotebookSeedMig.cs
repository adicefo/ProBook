using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class SharedNotebookSeedMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SharedNotebooks",
                columns: new[] { "Id", "FromUserId", "IsForEdit", "NotebookId", "SharedDate", "ToUserId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, false, 1, new DateTime(2025, 10, 13, 19, 49, 5, 0, DateTimeKind.Utc), 2, null },
                    { 2, 3, true, 14, new DateTime(2025, 10, 13, 11, 49, 5, 0, DateTimeKind.Utc), 2, null },
                    { 3, 2, false, 7, new DateTime(2025, 10, 11, 19, 49, 5, 0, DateTimeKind.Utc), 3, null },
                    { 4, 1, true, 4, new DateTime(2025, 10, 10, 11, 49, 5, 0, DateTimeKind.Utc), 3, null },
                    { 5, 3, false, 13, new DateTime(2025, 10, 9, 10, 49, 5, 0, DateTimeKind.Utc), 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SharedNotebooks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SharedNotebooks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SharedNotebooks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SharedNotebooks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SharedNotebooks",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
