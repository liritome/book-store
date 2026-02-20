using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class TranslatorService
    {
        private readonly BookStoreDbContext dbContext;

        public TranslatorService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Translator>> GetAllTranslatorsAsync()
        {
            return await dbContext.Translators.ToListAsync();
        }

        public async Task<List<Translator>> SearchTranslatorsAsync(string query)
        {
            var result = dbContext.Translators.AsQueryable();
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

        public async Task<Translator?> GetTranslatorByIdAsync(int id)
        {
            return await dbContext.Translators.FindAsync(id);
        }
    }
}
