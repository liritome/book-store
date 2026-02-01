using BookStore.Data;
using BookStore.Models;
using BookStore.InputModels;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class ProductService
    {
        private readonly BookStoreDbContext dbContext;

        public ProductService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Book
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await dbContext.Books
                .Include(b => b.Language)
                .Include(b => b.Series)
                .Include(b => b.Publisher)
                .Include(b => b.PhysicalBook.Discount)
                .Include(b => b.DigitalBook.Discount)
                .Include(b => b.Authors)
                .Include(b => b.Translators)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(BookAddInputModel model)
        {
            var book = new Book();

            book.Title = model.Title;
            book.Description = model.Description;
            book.Rating = 0;
            book.Reviews = new List<Review>();
            book.Images = new List<ProductImage>();
            book.IsVisible = model.IsVisible switch
            {
                "Visible" => true,
                "Hidden" => false,
                "Scheduled" => false,
                _ => false
            };

            book.Authors = await dbContext.Authors.Where(a => a.Id == model.AuthorId).ToListAsync();
            book.Translators = await dbContext.Translators.Where(t => t.Id == model.TranslatorId).ToListAsync();
            book.PublisherId = model.PublisherId;
            book.Year = model.Year;
            book.LanguageId = model.LanguageId;
            book.Tags = new List<Tag>();
            book.SeriesId = model.SeriesId;
            book.NumberInSeries = model.NumberInSeries;

            book.PhysicalBook = new PhysicalBook();
            book.PhysicalBook.Slug = model.Slug;
            book.PhysicalBook.CoverType = model.CoverType;
            book.PhysicalBook.Pages = model.Pages;
            book.PhysicalBook.Stock = model.Stock;
            book.PhysicalBook.IsAvailable = model.IsAvailable;
            book.PhysicalBook.Width = model.Width;
            book.PhysicalBook.Height = model.Height;
            book.PhysicalBook.Thickness = model.Thickness;
            book.PhysicalBook.Weight = model.Weight;
            book.PhysicalBook.Price = model.Price;
            book.PhysicalBook.FinalPrice = model.FinalPrice;
            book.PhysicalBook.DiscountId = model.DiscountId;
            book.PhysicalBook.Isbn = model.Isbn;

            if (model.HasDigitalBook)
            {
                book.DigitalBook = new DigitalBook();
                book.DigitalBook.Slug = model.DigitalSlug;
                book.DigitalBook.FileName = model.File == null ? "" : model.File.FileName;
                book.DigitalBook.IsAvailable = model.DigitalIsAvailable;
                book.DigitalBook.Price = model.DigitalPrice;
                book.DigitalBook.FinalPrice = model.DigitalFinalPrice;
                book.DigitalBook.DiscountId = model.DigitalDiscountId;
                book.DigitalBook.Isbn = model.DigitalIsbn;
            }

            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookEditInputModel model)
        {
            Book? book = await GetBookByIdAsync(model.BookId);
            if (book == null)
            {
                return;
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.Images = new List<ProductImage>();
            book.IsVisible = model.IsVisible switch
            {
                "Visible" => true,
                "Hidden" => false,
                "Scheduled" => false,
                _ => false
            };
            book.Authors = await dbContext.Authors.Where(a => a.Id == model.AuthorId).ToListAsync();
            book.Translators = await dbContext.Translators.Where(t => t.Id == model.TranslatorId).ToListAsync();
            book.PublisherId = model.PublisherId;
            book.Year = model.Year;
            book.LanguageId = model.LanguageId;
            book.Tags = new List<Tag>();
            book.SeriesId = model.SeriesId;
            book.NumberInSeries = model.NumberInSeries;

            book.PhysicalBook ??= new PhysicalBook();
            book.PhysicalBook.Slug = model.Slug;
            book.PhysicalBook.CoverType = model.CoverType;
            book.PhysicalBook.Pages = model.Pages;
            book.PhysicalBook.Stock = model.Stock;
            book.PhysicalBook.IsAvailable = model.IsAvailable;
            book.PhysicalBook.Width = model.Width;
            book.PhysicalBook.Height = model.Height;
            book.PhysicalBook.Thickness = model.Thickness;
            book.PhysicalBook.Weight = model.Weight;
            book.PhysicalBook.Price = model.Price;
            book.PhysicalBook.FinalPrice = model.FinalPrice;
            book.PhysicalBook.DiscountId = model.DiscountId;
            book.PhysicalBook.Isbn = model.Isbn;

            if (!model.HasDigitalBook)
            {
                book.DigitalBook = null;
            }
            else
            {
                book.DigitalBook ??= new DigitalBook();
                book.DigitalBook.Slug = model.DigitalSlug;
                book.DigitalBook.FileName = model.File == null ? "" : model.File.FileName;
                book.DigitalBook.IsAvailable = model.DigitalIsAvailable;
                book.DigitalBook.Price = model.DigitalPrice;
                book.DigitalBook.FinalPrice = model.DigitalFinalPrice;
                book.DigitalBook.DiscountId = model.DigitalDiscountId;
                book.DigitalBook.Isbn = model.DigitalIsbn;
            }

            dbContext.Books.Update(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book == null) return;
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task ToggleBookVisibility(int id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book == null) return;
            book.IsVisible = !book.IsVisible;
            await dbContext.SaveChangesAsync();
        }
    }
}
