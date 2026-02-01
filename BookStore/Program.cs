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

            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<LanguageService>();
            builder.Services.AddScoped<SeriesService>();
            builder.Services.AddScoped<PublisherService>();
            builder.Services.AddScoped<DiscountService>();
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<TranslatorService>();

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

            app.Run();
        }
    }
}
