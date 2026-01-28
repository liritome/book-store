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

        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            return await dbContext.Publishers.FindAsync(id);
        }
    }
}
