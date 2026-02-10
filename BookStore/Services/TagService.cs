using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class TagService
    {
        private readonly BookStoreDbContext dbContext;

        public TagService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await dbContext.Tags.FindAsync(id);
        }
    }
}
