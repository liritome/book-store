using BookStore.Data;
using BookStore.InputModels;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Services
{
    public class BookBaseService
    {
        private readonly BookStoreDbContext dbContext;

        public BookBaseService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<BookBase> GetAllBookBasesQuery(BookBaseFilter filter, BookBaseSortState sort)
        {
            IQueryable<BookBase> bases = dbContext.BookBases.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Query))
            {
                string query = filter.Query.Trim();
                bool queryIsNumber = int.TryParse(query, out int number);
                bases = bases
                    .Where(b => b.Title.Contains(query)
                        || (b.Series != null && b.Series.Name.Contains(query))
                        || (queryIsNumber && b.Id == number));
            }

            if (!filter.AuthorIds.IsNullOrEmpty())
            {
                bases = bases
                    .Where(b => filter.AuthorIds.All(id => b.Authors.Any(a => a.Id == id)));
            }

            if (!filter.TagIds.IsNullOrEmpty())
            {
                bases = bases
                    .Where(b => filter.TagIds.All(id => b.Tags.Any(t => t.Id == id)));
            }

            if (filter.YearFrom != null)
            {
                bases = bases
                    .Where(b => b.Year >= filter.YearFrom);
            }

            if (filter.YearTo != null)
            {
                bases = bases
                    .Where(b => b.Year <= filter.YearTo);
            }

            if (filter.EditionsCountFrom != null)
            {
                bases = bases
                    .Where(b => b.BookEditions.Count >= filter.EditionsCountFrom);
            }

            if (filter.EditionsCountTo != null)
            {
                bases = bases
                    .Where(b => b.BookEditions.Count <= filter.EditionsCountTo);
            }

            var basesOrdered = sort switch
            {
                BookBaseSortState.IdDesc => bases.OrderByDescending(b => b.Id),
                BookBaseSortState.TitleAsc => bases.OrderBy(b => b.Title),
                BookBaseSortState.TitleDesc => bases.OrderByDescending(b => b.Title),
                BookBaseSortState.SeriesIdAsc => bases.OrderBy(b => b.SeriesId),
                BookBaseSortState.SeriesIdDesc => bases.OrderByDescending(b => b.SeriesId),
                BookBaseSortState.YearAsc => bases.OrderBy(b => b.Year),
                BookBaseSortState.YearDesc => bases.OrderByDescending(b => b.Year),
                BookBaseSortState.EditionsCountAsc => bases.OrderBy(b => b.BookEditions.Count),
                BookBaseSortState.EditionsCountDesc => bases.OrderByDescending(b => b.BookEditions.Count),
                _ => bases.OrderBy(b => b.Id)
            };

            var basesIncluded = basesOrdered
                .Include(b => b.Series)
                .Include(b => b.Tags)
                .Include(b => b.Authors)
                .Include(b => b.BookEditions);

            return basesIncluded;
        }

        public async Task<List<BookBase>> GetBookBasesPageAsync(IQueryable<BookBase> bases, int page, int pageSize)
        {
            var result = bases
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            return await result.ToListAsync();
        }

        public async Task<BookBase?> GetBaseByIdAsync(int id)
        {
            return await dbContext.BookBases
                .Include(b => b.Authors)
                .Include(b => b.Series)
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        /*public async Task<int> AddBookBaseAsync(BookBaseAddInputModel model)
        {

        }*/
    }
}
