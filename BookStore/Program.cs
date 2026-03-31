using BookStore.Data;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var culture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));

            builder.Services.AddScoped<BookBaseService>();
            builder.Services.AddScoped<BookEditionService>();
            builder.Services.AddScoped<PhysicalBookService>();
            builder.Services.AddScoped<LanguageService>();
            builder.Services.AddScoped<SeriesService>();
            builder.Services.AddScoped<PublisherService>();
            builder.Services.AddScoped<DiscountService>();
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<TranslatorService>();
            builder.Services.AddScoped<TagService>();
            builder.Services.AddScoped<ProductImageService>();
            builder.Services.AddScoped<SlugService>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            /*
             * todo: add close btn for SelectInput partial, that will close Searched div
             * todo: fix final price autocomplete for Book add/edit view
             * todo: add Series to BookBase filters
             * todo: Rework authors column in BookBase Index table for better ui design
            */

            app.Run();
        }
    }
}
