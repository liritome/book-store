using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class RenameIsbnProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhysicalBook_ISBN",
                table: "Books",
                newName: "PhysicalBook_Isbn");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_ISBN",
                table: "Books",
                newName: "DigitalBook_Isbn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Isbn",
                table: "Books",
                newName: "PhysicalBook_ISBN");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_Isbn",
                table: "Books",
                newName: "DigitalBook_ISBN");
        }
    }
}
