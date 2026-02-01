using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class TranslatorService
    {
        private BookStoreDbContext dbContext;

        public TranslatorService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Translator>> GetAllTranslatorsAsync()
        {
            return await dbContext.Translators.ToListAsync();
        }

        public async Task<Translator?> GetTranslatorByIdAsync(int id)
        {
            return await dbContext.Translators.FindAsync(id);
        }
    }
}
