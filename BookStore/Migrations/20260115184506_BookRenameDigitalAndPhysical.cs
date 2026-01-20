using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class BookRenameDigitalAndPhysical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Discounts_DigitalBookDetails_DiscountId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Discounts_PhysicalBookDetails_DiscountId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Width",
                table: "Books",
                newName: "PhysicalBook_Width");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Weight",
                table: "Books",
                newName: "PhysicalBook_Weight");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Thickness",
                table: "Books",
                newName: "PhysicalBook_Thickness");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Stock",
                table: "Books",
                newName: "PhysicalBook_Stock");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Slug",
                table: "Books",
                newName: "PhysicalBook_Slug");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Price",
                table: "Books",
                newName: "PhysicalBook_Price");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Pages",
                table: "Books",
                newName: "PhysicalBook_Pages");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_IsAvailable",
                table: "Books",
                newName: "PhysicalBook_IsAvailable");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_ISBN",
                table: "Books",
                newName: "PhysicalBook_ISBN");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_Height",
                table: "Books",
                newName: "PhysicalBook_Height");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_FinalPrice",
                table: "Books",
                newName: "PhysicalBook_FinalPrice");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_DiscountId",
                table: "Books",
                newName: "PhysicalBook_DiscountId");

            migrationBuilder.RenameColumn(
                name: "PhysicalBookDetails_CoverType",
                table: "Books",
                newName: "PhysicalBook_CoverType");

            migrationBuilder.RenameColumn(
                name: "DigitalBookDetails_Slug",
                table: "Books",
                newName: "DigitalBook_Slug");

            migrationBuilder.RenameColumn(
                name: "DigitalBookDetails_Price",
                table: "Books",
                newName: "DigitalBook_Price");

            migrationBuilder.RenameColumn(
                name: "DigitalBookDetails_IsAvailable",
                table: "Books",
                newName: "DigitalBook_IsAvailable");

            migrationBuilder.RenameColumn(
                name: "DigitalBookDetails_ISBN",
                table: "Books",
                newName: "DigitalBook_ISBN");

            migrationBuilder.RenameColumn(
                name: "DigitalBookDetails_FinalPrice",
                table: "Books",
                newName: "DigitalBook_FinalPrice");

            migrationBuilder.RenameColumn(
                name: "DigitalBookDetails_FileName",
                table: "Books",
                newName: "DigitalBook_FileName");

            migrationBuilder.RenameColumn(
                name: "DigitalBookDetails_DiscountId",
                table: "Books",
                newName: "DigitalBook_DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PhysicalBookDetails_DiscountId",
                table: "Books",
                newName: "IX_Books_PhysicalBook_DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_DigitalBookDetails_DiscountId",
                table: "Books",
                newName: "IX_Books_DigitalBook_DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Discounts_DigitalBook_DiscountId",
                table: "Books",
                column: "DigitalBook_DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Discounts_PhysicalBook_DiscountId",
                table: "Books",
                column: "PhysicalBook_DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Discounts_DigitalBook_DiscountId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Discounts_PhysicalBook_DiscountId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Width",
                table: "Books",
                newName: "PhysicalBookDetails_Width");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Weight",
                table: "Books",
                newName: "PhysicalBookDetails_Weight");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Thickness",
                table: "Books",
                newName: "PhysicalBookDetails_Thickness");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Stock",
                table: "Books",
                newName: "PhysicalBookDetails_Stock");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Slug",
                table: "Books",
                newName: "PhysicalBookDetails_Slug");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Price",
                table: "Books",
                newName: "PhysicalBookDetails_Price");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Pages",
                table: "Books",
                newName: "PhysicalBookDetails_Pages");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_IsAvailable",
                table: "Books",
                newName: "PhysicalBookDetails_IsAvailable");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_ISBN",
                table: "Books",
                newName: "PhysicalBookDetails_ISBN");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_Height",
                table: "Books",
                newName: "PhysicalBookDetails_Height");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_FinalPrice",
                table: "Books",
                newName: "PhysicalBookDetails_FinalPrice");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_DiscountId",
                table: "Books",
                newName: "PhysicalBookDetails_DiscountId");

            migrationBuilder.RenameColumn(
                name: "PhysicalBook_CoverType",
                table: "Books",
                newName: "PhysicalBookDetails_CoverType");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_Slug",
                table: "Books",
                newName: "DigitalBookDetails_Slug");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_Price",
                table: "Books",
                newName: "DigitalBookDetails_Price");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_IsAvailable",
                table: "Books",
                newName: "DigitalBookDetails_IsAvailable");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_ISBN",
                table: "Books",
                newName: "DigitalBookDetails_ISBN");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_FinalPrice",
                table: "Books",
                newName: "DigitalBookDetails_FinalPrice");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_FileName",
                table: "Books",
                newName: "DigitalBookDetails_FileName");

            migrationBuilder.RenameColumn(
                name: "DigitalBook_DiscountId",
                table: "Books",
                newName: "DigitalBookDetails_DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PhysicalBook_DiscountId",
                table: "Books",
                newName: "IX_Books_PhysicalBookDetails_DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_DigitalBook_DiscountId",
                table: "Books",
                newName: "IX_Books_DigitalBookDetails_DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Discounts_DigitalBookDetails_DiscountId",
                table: "Books",
                column: "DigitalBookDetails_DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Discounts_PhysicalBookDetails_DiscountId",
                table: "Books",
                column: "PhysicalBookDetails_DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }
    }
}
