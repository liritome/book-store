using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options) { }

        public DbSet<BookBase> BookBases { get; set; }
        public DbSet<BookEdition> BookEditions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PhysicalBook> PhysicalBooks { get; set; }
        public DbSet<DigitalBook> DigitalBooks { get; set; }
        public DbSet<Merch> Merch { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Series> Series { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AdminPermission> AdminPermissions { get; set; }
        public DbSet<AdminAction> AdminActions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<PhysicalBook>().ToTable("PhysicalBooks");
            modelBuilder.Entity<DigitalBook>().ToTable("DigitalBooks");
            modelBuilder.Entity<Merch>().ToTable("Merch");

            modelBuilder.Entity<Discount>()
                .HasDiscriminator<string>("Type")
                .HasValue<PercentageDiscount>("Percentage")
                .HasValue<AmountDiscount>("Amount");
        }
    }
}
