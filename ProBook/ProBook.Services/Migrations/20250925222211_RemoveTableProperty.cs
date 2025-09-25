using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Table",
                table: "Pages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Table",
                table: "Pages",
                type: "jsonb",
                nullable: true);
        }
    }
}
