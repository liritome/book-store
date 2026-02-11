using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class ProductImageService
    {
        private readonly BookStoreDbContext dbContext;
        private readonly string imagesPath;

        public ProductImageService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
            imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "productImages");
        }

        public async Task<ProductImage?> GetImageById(int id)
        {
            return await dbContext.ProductImages.FindAsync(id);
        }

        public async Task<ProductImage> AddImageAsync(Product product, IFormFile image)
        {
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }
            string imageNewName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            string imageNewPath = Path.Combine(imagesPath, imageNewName);

            using (var stream = new FileStream(imageNewPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var productImage = new ProductImage()
            {
                Url = imageNewName,
                Number = product.Images.Count
            };
            dbContext.ProductImages.Add(productImage);

            await dbContext.SaveChangesAsync();
            return productImage;
        }

        public async Task EditImageNumberAsync(int id, int number)
        {
            var image = await GetImageById(id);
            if (image == null) return;            
            image.Number = number;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(int id)
        {
            var image = await GetImageById(id);
            if (image == null) return;
            string imagePath = Path.Combine(imagesPath, image.Url);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            dbContext.ProductImages.Remove(image);
            await dbContext.SaveChangesAsync();
        }
    }
}
