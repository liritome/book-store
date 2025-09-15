using BookStore.Interfaces;

namespace BookStore.Models
{
    public class Merch : IPhysicalProduct
    {
        // IProduct
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Discount? Discount { get; set; }
        public double Rating { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<string> Images { get; set; } = new List<string>();
        public bool IsVisible { get; set; }

        // IPhysicalProduct
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Stock { get; set; }
        public string GetSize() => $"{Width}x{Length}x{Height} mm";
        public bool IsInStock() => Stock > 0;
    }
}
