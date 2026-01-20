using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class ReworkImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_ImageId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Images_ImageId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_ImageId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ImageId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Publishers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Publishers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_ImageId",
                table: "Publishers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ImageId",
                table: "Authors",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_ImageId",
                table: "Authors",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Images_ImageId",
                table: "Publishers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
