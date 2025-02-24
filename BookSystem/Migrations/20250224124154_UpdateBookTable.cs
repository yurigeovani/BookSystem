using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Books",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Books",
                newName: "createdAt");
        }
    }
}
