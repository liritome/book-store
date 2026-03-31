using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class DiscountService
    {
        private readonly BookStoreDbContext dbContext;

        public DiscountService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Discount>> GetAllDiscountsAsync()
        {
            return await dbContext.Discounts.ToListAsync();
        }

        public async Task<List<Discount>> GetActiveDiscountsAsync()
        {
            var now = DateTime.UtcNow;
            return await dbContext.Discounts
                .Where(d => d.ValidFrom <= now && d.ValidTo >= now)
                .OrderBy(d => d.ValidFrom)
                .ToListAsync();
        }

        public async Task<List<Discount>> GetActiveAndScheduledDiscountsAsync()
        {
            var now = DateTime.UtcNow;
            return await dbContext.Discounts
                .Where(d => d.ValidTo >= now)
                .OrderBy(d => d.ValidFrom > now)
                .ThenBy(d => d.ValidFrom)
                .ToListAsync();
        }

        public async Task<List<Discount>> SearchDiscountsAsync(string query)
        {
            var result = dbContext.Discounts.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                query = query.Trim();
                result = result
                    .Where(d => d.Name.Contains(query));
            }
            return await result
                .OrderBy(d => d.Name)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Discount?> GetDiscountByIdAsync(int id)
        {
            return await dbContext.Discounts.FindAsync(id);
        }
    }
}
