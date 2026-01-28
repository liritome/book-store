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

        public async Task<Discount?> GetDiscountByIdAsync(int id)
        {
            return await dbContext.Discounts.FindAsync(id);
        }
    }
}
