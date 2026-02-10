using BookStore.Data;
using BookStore.Models;
using BookStore.InputModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<PhysicalBook>> GetAllBooksAsync()
        {
            return await dbContext.PhysicalBooks
                .Include(b => b.BookEdition)
                    .ThenInclude(e => e.BookBase)
                .Include(b => b.BookEdition)
                    .ThenInclude(e => e.BookBase)
                        .ThenInclude(bb => bb.Series)
                .Include(b => b.Images)
                .ToListAsync();
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
            //book.Images = new List<ProductImage>();
            //book.Reviews = new List<Review>();
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
            if (book == null) return;

            var images = await productImageService.CreateImagesAsync(files);
            await productImageService.DeleteImagesAsync(book.Images);
            book.Images.Clear();
            foreach(var image in images)
            {
                book.Images.Add(image);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task ReorderImagesAsync(List<ImageOrder> imageOrders)
        {
            foreach (var item in imageOrders)
            {
                var image = dbContext.ProductImages.First(i => i.Id == item.ImageId);
                image.Number = item.Order;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await dbContext.PhysicalBooks.FindAsync(id);
            if (book == null) return;
            dbContext.PhysicalBooks.Remove(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task ToggleBookVisibilityAsync(int id)
        {
            var book = await dbContext.PhysicalBooks.FindAsync(id);
            if (book == null) return;
            book.IsVisible = !book.IsVisible;
            await dbContext.SaveChangesAsync();
        }
    }
}
