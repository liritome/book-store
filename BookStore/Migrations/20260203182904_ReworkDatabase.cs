using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class ReworkDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchItems_Discounts_DiscountId",
                table: "MerchItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MerchItems_Products_Id",
                table: "MerchItems");

            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookTag");

            migrationBuilder.DropTable(
                name: "BookTranslator");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MerchItems",
                table: "MerchItems");

            migrationBuilder.DropIndex(
                name: "IX_MerchItems_DiscountId",
                table: "MerchItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "MerchItems");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "MerchItems");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "MerchItems");

            migrationBuilder.RenameTable(
                name: "MerchItems",
                newName: "Merch");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Merch",
                newName: "Stock");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalPrice",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Products",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Merch",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Merch",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merch",
                table: "Merch",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BookBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    SeriesId = table.Column<int>(type: "int", nullable: true),
                    NumberInSeries = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookBases_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorBookBase",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BookBasesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBookBase", x => new { x.AuthorsId, x.BookBasesId });
                    table.ForeignKey(
                        name: "FK_AuthorBookBase_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBookBase_BookBases_BookBasesId",
                        column: x => x.BookBasesId,
                        principalTable: "BookBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBaseTag",
                columns: table => new
                {
                    BookBasesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBaseTag", x => new { x.BookBasesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BookBaseTag_BookBases_BookBasesId",
                        column: x => x.BookBasesId,
                        principalTable: "BookBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBaseTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookEditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookBaseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookEditions_BookBases_BookBaseId",
                        column: x => x.BookBaseId,
                        principalTable: "BookBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookEditions_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookEditions_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookEditionTranslator",
                columns: table => new
                {
                    BookEditionsId = table.Column<int>(type: "int", nullable: false),
                    TranslatorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEditionTranslator", x => new { x.BookEditionsId, x.TranslatorsId });
                    table.ForeignKey(
                        name: "FK_BookEditionTranslator_BookEditions_BookEditionsId",
                        column: x => x.BookEditionsId,
                        principalTable: "BookEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookEditionTranslator_Translators_TranslatorsId",
                        column: x => x.TranslatorsId,
                        principalTable: "Translators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BookEditionId = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalBooks_BookEditions_BookEditionId",
                        column: x => x.BookEditionId,
                        principalTable: "BookEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigitalBooks_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BookEditionId = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAddition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionAddition = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    CoverType = table.Column<int>(type: "int", nullable: false),
                    HasIllustrations = table.Column<bool>(type: "bit", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Thickness = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalBooks_BookEditions_BookEditionId",
                        column: x => x.BookEditionId,
                        principalTable: "BookEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhysicalBooks_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountId",
                table: "Products",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookBase_BookBasesId",
                table: "AuthorBookBase",
                column: "BookBasesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBases_SeriesId",
                table: "BookBases",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBaseTag_TagsId",
                table: "BookBaseTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookEditions_BookBaseId",
                table: "BookEditions",
                column: "BookBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_BookEditions_LanguageId",
                table: "BookEditions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_BookEditions_PublisherId",
                table: "BookEditions",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BookEditionTranslator_TranslatorsId",
                table: "BookEditionTranslator",
                column: "TranslatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalBooks_BookEditionId",
                table: "DigitalBooks",
                column: "BookEditionId",
                unique: true,
                filter: "[BookEditionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalBooks_BookEditionId",
                table: "PhysicalBooks",
                column: "BookEditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Merch_Products_Id",
                table: "Merch",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merch_Products_Id",
                table: "Merch");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "AuthorBookBase");

            migrationBuilder.DropTable(
                name: "BookBaseTag");

            migrationBuilder.DropTable(
                name: "BookEditionTranslator");

            migrationBuilder.DropTable(
                name: "DigitalBooks");

            migrationBuilder.DropTable(
                name: "PhysicalBooks");

            migrationBuilder.DropTable(
                name: "BookEditions");

            migrationBuilder.DropTable(
                name: "BookBases");

            migrationBuilder.DropIndex(
                name: "IX_Products_DiscountId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merch",
                table: "Merch");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Merch");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Merch");

            migrationBuilder.RenameTable(
                name: "Merch",
                newName: "MerchItems");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "MerchItems",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "MerchItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalPrice",
                table: "MerchItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "MerchItems",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MerchItems",
                table: "MerchItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    SeriesId = table.Column<int>(type: "int", nullable: true),
                    NumberInSeries = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DigitalBook_DiscountId = table.Column<int>(type: "int", nullable: true),
                    DigitalBook_FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DigitalBook_FinalPrice = table.Column<int>(type: "int", nullable: true),
                    DigitalBook_IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    DigitalBook_Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DigitalBook_Price = table.Column<int>(type: "int", nullable: true),
                    DigitalBook_Slug = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    PhysicalBook_DiscountId = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_CoverType = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_FinalPrice = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_Height = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    PhysicalBook_Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhysicalBook_Pages = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_Price = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_Slug = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    PhysicalBook_Stock = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_Thickness = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_Weight = table.Column<int>(type: "int", nullable: true),
                    PhysicalBook_Width = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Discounts_DigitalBook_DiscountId",
                        column: x => x.DigitalBook_DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Discounts_PhysicalBook_DiscountId",
                        column: x => x.PhysicalBook_DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTag",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTag", x => new { x.BooksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BookTag_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTranslator",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    TranslatorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTranslator", x => new { x.BooksId, x.TranslatorsId });
                    table.ForeignKey(
                        name: "FK_BookTranslator_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTranslator_Translators_TranslatorsId",
                        column: x => x.TranslatorsId,
                        principalTable: "Translators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MerchItems_DiscountId",
                table: "MerchItems",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_DigitalBook_DiscountId",
                table: "Books",
                column: "DigitalBook_DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageId",
                table: "Books",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PhysicalBook_DiscountId",
                table: "Books",
                column: "PhysicalBook_DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SeriesId",
                table: "Books",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTag_TagsId",
                table: "BookTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTranslator_TranslatorsId",
                table: "BookTranslator",
                column: "TranslatorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MerchItems_Discounts_DiscountId",
                table: "MerchItems",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MerchItems_Products_Id",
                table: "MerchItems",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
