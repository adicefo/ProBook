using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProBook.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentSharedNotebookCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SharedNotebooks_SharedNotebookId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "SharedNotebookId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SharedNotebooks_SharedNotebookId",
                table: "Comments",
                column: "SharedNotebookId",
                principalTable: "SharedNotebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SharedNotebooks_SharedNotebookId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "SharedNotebookId",
                table: "Comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SharedNotebooks_SharedNotebookId",
                table: "Comments",
                column: "SharedNotebookId",
                principalTable: "SharedNotebooks",
                principalColumn: "Id");
        }
    }
}
