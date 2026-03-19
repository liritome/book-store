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
        private readonly SlugService slugService;

        public PhysicalBookService(BookStoreDbContext dbContext, ProductImageService productImageService, SlugService slugService)
        {
            this.dbContext = dbContext;
            this.productImageService = productImageService;
            this.slugService = slugService;
        }

        public IQueryable<PhysicalBook> GetAllBooksQuery(PhysicalBookFilter filter, PhysicalBookSortState sort)
        {
            IQueryable<PhysicalBook> books = dbContext.PhysicalBooks.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Query))
            {
                string query = filter.Query.Trim();
                bool queryIsNumber = int.TryParse(query, out int number);
                books = books
                    .Where(b => b.BookEdition.Title.Contains(query)
                        || (b.BookEdition.BookBase.Series != null && b.BookEdition.BookBase.Series.Name.Contains(query) == true)
                        || b.Isbn.Contains(query)
                        || (queryIsNumber && b.Id == number));
            }

            if (!filter.AuthorIds.IsNullOrEmpty())
            {
                books = books
                    .Where(b => filter.AuthorIds.All(id => b.BookEdition.BookBase.Authors.Any(a => a.Id == id)));
            }

            if (!filter.PublisherIds.IsNullOrEmpty())
            {
                books = books
                    .Where(b => filter.PublisherIds.Contains(b.BookEdition.PublisherId));
            }

            if (!filter.TranslatorIds.IsNullOrEmpty())
            {
                books = books.
                    Where(b => filter.TranslatorIds.All(id => b.BookEdition.Translators.Any(t => t.Id == id)));
            }

            if (filter.YearFrom != null)
            {
                books = books
                    .Where(b => b.BookEdition.BookBase.Year >= filter.YearFrom);
            }

            if (filter.YearTo != null)
            {
                books = books
                    .Where(b => b.BookEdition.BookBase.Year <= filter.YearTo);
            }

            if (filter.PagesFrom != null)
            {
                books = books
                    .Where(b => b.Pages >= filter.PagesFrom);
            }

            if (filter.PagesTo != null)
            {
                books = books
                    .Where(b => b.Pages <= filter.PagesTo);
            }

            if (filter.StockFrom != null)
            {
                books = books
                    .Where(b => b.Stock >= filter.StockFrom);
            }

            if (filter.StockTo != null)
            {
                books = books
                    .Where(b => b.Stock <= filter.StockTo);
            }

            if (!filter.LanguageIds.IsNullOrEmpty())
            {
                books = books
                    .Where(b => filter.LanguageIds.Contains(b.BookEdition.LanguageId));
            }

            if (!filter.Visibility.IsNullOrEmpty())
            {
                books = books
                    .Where(b =>
                        (filter.Visibility.Contains("visible") && b.IsVisible) ||
                        (filter.Visibility.Contains("hidden") && !b.IsVisible));
            }

            if (!filter.CoverTypes.IsNullOrEmpty())
            {
                books = books
                    .Where(b => filter.CoverTypes.Contains(b.CoverType));
            }

            if (filter.PriceFrom != null)
            {
                books = books
                    .Where(b => b.Price >= filter.PriceFrom);
            }

            if (filter.PriceTo != null)
            {
                books = books
                    .Where(b => b.Price <= filter.PriceTo);
            }

            if (filter.DiscountId != null)
            {
                books = filter.DiscountId switch
                {
                    -2 => books.Where(b => b.DiscountId != null),
                    -1 => books.Where(b => b.DiscountId == null),
                    _ => books.Where(b => b.DiscountId == filter.DiscountId)
                };
            }

            if (filter.FinalFrom != null)
            {
                books = books
                    .Where(b => b.FinalPrice >= filter.FinalFrom);
            }

            if (filter.FinalTo != null)
            {
                books = books
                    .Where(b => b.FinalPrice <= filter.FinalTo);
            }

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

            var booksIncluded = booksOrdered
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.BookBase)
                        .ThenInclude(bb => bb.Authors)
                .Include(b => b.BookEdition)
                    .ThenInclude(be => be.BookBase)
                        .ThenInclude(bb => bb.Series)
                .Include(b => b.Discount)
                .Include(b => b.Images);

            return booksIncluded;
        }

        public async Task<List<PhysicalBook>> GetBooksPageAsync(IQueryable<PhysicalBook> books, int page, int pageSize)
        {
            var result = books
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

            return await result.ToListAsync();
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

        public async Task<int> AddBookAsync(PhysicalBookAddInputModel model, string bookEditionTitle)
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

            book.Slug = await slugService.GenerateUniqueAsync<Product>($"{bookEditionTitle} {model.TitleAddition}");
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

        public async Task UpdateBookAsync(PhysicalBookEditInputModel model, string bookEditionTitle)
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

            book.Slug = await slugService.GenerateUniqueAsync<Product>($"{bookEditionTitle} {model.TitleAddition}");
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
