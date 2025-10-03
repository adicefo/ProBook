using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class CommentSharedNotebookRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SharedNotebookId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SharedNotebookId",
                table: "Comments",
                column: "SharedNotebookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SharedNotebooks_SharedNotebookId",
                table: "Comments",
                column: "SharedNotebookId",
                principalTable: "SharedNotebooks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SharedNotebooks_SharedNotebookId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SharedNotebookId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SharedNotebookId",
                table: "Comments");
        }
    }
}
