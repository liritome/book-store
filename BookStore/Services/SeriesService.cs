using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class SeriesService
    {
        private readonly BookStoreDbContext dbContext;

        public SeriesService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Series>> GetAllSeriesAsync()
        {
            return await dbContext.Series.ToListAsync();
        }

        public async Task<List<Series>> SearchSeriesAsync(string query)
        {
            var result = dbContext.Series.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                result = result
                    .Where(s => s.Name.Contains(query));
            }
            return await result
                .OrderBy(s => s.Name)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Series?> GetSeriesByIdAsync(int id)
        {
            return await dbContext.Series.FindAsync(id);
        }
    }
}
