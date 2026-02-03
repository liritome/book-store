using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookBaseService
    {
        private readonly BookStoreDbContext dbContext;

        public BookBaseService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<BookBase>> GetAllBasesAsync()
        {
            return await dbContext.BookBases.ToListAsync();
        }

        public async Task<BookBase?> GetBaseByIdAsync(int id)
        {
            return await dbContext.BookBases.FindAsync(id);
        }
    }
}
