using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using BookStore.Interfaces;

namespace BookStore.Services
{
    public class SlugService
    {
        private readonly BookStoreDbContext dbContext;

        public SlugService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Generate(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach(char c in normalized)
            {
                var unicodeCategory = char.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            string result = sb.ToString().Normalize(NormalizationForm.FormC);
            result = result.ToLower();
            result = Regex.Replace(result, @"\s+", "-");
            result = Regex.Replace(result, @"-+", "-").Trim('-');
            return result;
        }

        public async Task<string> GenerateUniqueAsync<T>(string input) where T : class, ISlug
        {
            string slug = Generate(input);
            string result = slug;
            int i = 2;
            while (await dbContext.Set<T>().AnyAsync(e => e.Slug == result))
            {
                result = $"{slug}-{i++}";
            }
            return result;
        }
    }
}
