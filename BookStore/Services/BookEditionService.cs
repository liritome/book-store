using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookEditionService
    {
        private readonly BookStoreDbContext dbContext;

        public BookEditionService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<BookEdition>> GetAllEditionsAsync()
        {
            return await dbContext.BookEditions.Include(e => e.Publisher).ToListAsync();
        }

        public async Task<BookEdition?> GetEditionByIdAsync(int id)
        {
            return await dbContext.BookEditions.FindAsync(id);
        }
    }
}
