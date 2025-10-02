using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class CommentViewProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Viewed",
                table: "Comments",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "Comments");
        }
    }
}
