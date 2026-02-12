using BookStore.Data;
using BookStore.Models;
using BookStore.InputModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Services
{
    public class PhysicalBookService
    {
        private readonly BookStoreDbContext dbContext;
        private readonly ProductImageService productImageService;

        public PhysicalBookService(BookStoreDbContext dbContext, ProductImageService productImageService)
        {
            this.dbContext = dbContext;
            this.productImageService = productImageService;
        }

        public async Task<List<PhysicalBook>> GetAllBooksAsync(PhysicalBookSortState sort)
        {
            var books = dbContext.PhysicalBooks
                .Include(b => b.BookEdition)
                    .ThenInclude(e => e.BookBase)
                .Include(b => b.BookEdition)
                    .ThenInclude(e => e.BookBase)
                        .ThenInclude(bb => bb.Series)
                .Include(b => b.Images);

            var booksOrdered = sort switch
            {
                PhysicalBookSortState.IdDesc => books.OrderByDescending(b => b.Id),
                PhysicalBookSortState.TitleAsc => books.OrderBy(b => b.BookEdition.Title),
                PhysicalBookSortState.TitleDesc => books.OrderByDescending(b => b.BookEdition.Title),
                PhysicalBookSortState.CoverAsc => books.OrderBy(b => b.CoverType),
                PhysicalBookSortState.CoverDesc => books.OrderByDescending(b => b.CoverType),
                PhysicalBookSortState.IsbnAsc => books.OrderBy(b => b.Isbn),
                PhysicalBookSortState.IsbnDesc => books.OrderByDescending(b => b.Isbn),
                PhysicalBookSortState.PriceAsc => books.OrderBy(b => b.Price),
                PhysicalBookSortState.PriceDesc => books.OrderByDescending(b => b.Price),
                PhysicalBookSortState.DiscountAsc => books.OrderBy(b => b.DiscountId),
                PhysicalBookSortState.DiscountDesc => books.OrderByDescending(b => b.DiscountId),
                PhysicalBookSortState.FinalPriceAsc => books.OrderBy(b => b.FinalPrice),
                PhysicalBookSortState.FinalPriceDesc => books.OrderByDescending(b => b.FinalPrice),
                PhysicalBookSortState.StockAsc => books.OrderBy(b => b.Stock),
                PhysicalBookSortState.StockDesc => books.OrderByDescending(b => b.Stock),
                _ => books.OrderBy(b => b.Id)
            };
            return await booksOrdered.ToListAsync();
        }

        public async Task<PhysicalBook?> GetBookByIdAsync(int id)
        {
            return await dbContext.PhysicalBooks
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.BookBase)
                        .ThenInclude(bb => bb.Authors)
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.BookBase)
                        .ThenInclude(bb => bb.Tags)
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.BookBase)
                        .ThenInclude(bb => bb.Series)
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.Language)
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.Publisher)
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.Translators)
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.PhysicalBooks)
                .Include(b => b.Discount)
                .Include(b => b.Images)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> AddBookAsync(PhysicalBookAddInputModel model)
        {
            var book = new PhysicalBook();

            book.BookEditionId = model.BookEditionId;
            book.Isbn = model.Isbn;
            book.TitleAddition = model.TitleAddition;
            book.DescriptionAddition = model.DescriptionAddition;
            book.CoverType = model.CoverType;
            book.HasIllustrations = model.HasIllustrations;
            book.Pages = model.Pages;
            book.Width = model.Width;
            book.Height = model.Height;
            book.Thickness = model.Thickness;
            book.Weight = model.Weight;
            book.Stock = model.Stock;

            book.Slug = model.Slug;
            book.Images = new List<ProductImage>();
            book.Reviews = new List<Review>();
            book.Price = model.Price;
            book.DiscountId = model.DiscountId;
            book.FinalPrice = model.FinalPrice;
            book.IsVisible = model.IsVisible switch
            {
                "Visible" => true,
                "Hidden" => false,
                _ => false
            };
            book.IsAvailable = model.IsAvailable;

            dbContext.PhysicalBooks.Add(book);
            await dbContext.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateBookAsync(PhysicalBookEditInputModel model)
        {
            PhysicalBook? book = await GetBookByIdAsync(model.BookId);
            if (book == null) return;

            book.BookEditionId = model.BookEditionId;
            book.Isbn = model.Isbn;
            book.TitleAddition = model.TitleAddition;
            book.DescriptionAddition = model.DescriptionAddition;
            book.CoverType = model.CoverType;
            book.HasIllustrations = model.HasIllustrations;
            book.Pages = model.Pages;
            book.Width = model.Width;
            book.Height = model.Height;
            book.Thickness = model.Thickness;
            book.Weight = model.Weight;
            book.Stock = model.Stock;

            book.Slug = model.Slug;
            book.Price = model.Price;
            book.DiscountId = model.DiscountId;
            book.FinalPrice = model.FinalPrice;
            book.IsVisible = model.IsVisible switch
            {
                "Visible" => true,
                "Hidden" => false,
                _ => false
            };
            book.IsAvailable = model.IsAvailable;

            dbContext.PhysicalBooks.Update(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task AttachImagesToBookAsync(int id, List<IFormFile> files)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null || files.IsNullOrEmpty()) return;

            foreach (var file in files)
            {
                var image = await productImageService.AddImageAsync(book, file);
                book.Images.Add(image);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task ReorderImagesAsync(List<ImageDto> imageDtos)
        {
            foreach (var item in imageDtos)
            {
                if (item.Delete)
                {
                    await productImageService.DeleteImageAsync(item.ImageId);
                }
                else
                {
                    await productImageService.EditImageNumberAsync(item.ImageId, item.Order);
                }
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null) return;
            dbContext.PhysicalBooks.Remove(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task ToggleBookVisibilityAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null) return;
            book.IsVisible = !book.IsVisible;
            await dbContext.SaveChangesAsync();
        }
    }
}
