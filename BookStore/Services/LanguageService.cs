using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class LanguageService
    {
        private readonly BookStoreDbContext dbContext;

        public LanguageService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Language>> GetAllLanguagesAsync()
        {
            return await dbContext.Languages.ToListAsync();
        }

        public async Task<List<Language>> SearchLanguagesAsync(string query)
        {
            var result = dbContext.Languages.AsQueryable();
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

        public async Task<Language?> GetLanguageByIdAsync(int id)
        {
            return await dbContext.Languages.FindAsync(id);
        }
    }
}
