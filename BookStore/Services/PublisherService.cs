using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class PublisherService
    {
        private readonly BookStoreDbContext dbContext;

        public PublisherService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Publisher>> GetAllPublishersAsync()
        {
            return await dbContext.Publishers.ToListAsync();
        }

        public async Task<List<Publisher>> SearchPublishersAsync(string query)
        {
            var result = dbContext.Publishers.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                query = query.Trim();
                result = result
                    .Where(a => a.Name.Contains(query));
            }
            return await result
                .OrderBy(a => a.Name)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            return await dbContext.Publishers.FindAsync(id);
        }
    }
}
