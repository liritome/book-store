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

        /*public async Task<List<ProductImage>> GetAllImagesByProductIdAsync(int id)
        {
            return await dbContext.ProductImages
                .Where(i => i.ProductId == id)
                .OrderBy(i => i.Number)
                .ToListAsync();
        }

        public async Task<ProductImage?> GetCoverImageByProductId(int id)
        {
            return await dbContext.ProductImages
                .Where(i => i.ProductId == id)
                .OrderBy(i => i.Number)
                .FirstOrDefaultAsync();
        }*/

        public async Task<ProductImage?> GetImageById(int id)
        {
            return await dbContext.ProductImages.FindAsync(id);
        }

        public async Task<List<ProductImage>> CreateImagesAsync(List<IFormFile> images)
        {
            List<ProductImage> productImages = new List<ProductImage>();
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            for (int i = 0; i < images.Count; i++)
            {
                var image = images[i];
                string imageNewName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                string imageNewPath = Path.Combine(imagesPath, imageNewName);

                using (var stream = new FileStream(imageNewPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var productImage = new ProductImage()
                {
                    Url = imageNewName,
                    Number = i
                };
                productImages.Add(productImage);
                dbContext.ProductImages.Add(productImage);
            }

            await dbContext.SaveChangesAsync();
            return productImages;
        }

        public async Task DeleteImagesAsync(ICollection<ProductImage> images)
        {
            foreach (var image in images)
            {
                string imagePath = Path.Combine(imagesPath, image.Url);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                dbContext.ProductImages.Remove(image);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
