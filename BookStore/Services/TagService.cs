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

        public async Task<List<Tag>> SearchTagsAsync(string query)
        {
            var result = dbContext.Tags.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                query = query.Trim();
                result = result
                    .Where(t => t.Name.Contains(query));
            }
            return await result
                .OrderBy(t => t.Name)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await dbContext.Tags.FindAsync(id);
        }
    }
}
