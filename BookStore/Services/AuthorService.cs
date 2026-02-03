using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class AuthorService
    {
        private readonly BookStoreDbContext dbContext;

        public AuthorService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await dbContext.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await dbContext.Authors.FindAsync(id);
        }
    }
}
