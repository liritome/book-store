using BookStore.Data;
using BookStore.Models;
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
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
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
            if(book == null) return;
            book.IsVisible = !book.IsVisible;
            await dbContext.SaveChangesAsync();
        }
    }
}
