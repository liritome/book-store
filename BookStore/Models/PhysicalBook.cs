using BookStore.Interfaces;

namespace BookStore.Models
{
    public class PhysicalBook : IPhysicalProduct, IBook
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

        // IBook
        public Author Author { get; set; } = null!;
        public Translator? Translator { get; set; } = null!;
        public string ISBN { get; set; } = string.Empty;
        public Publisher Publisher { get; set; } = null!;
        public DateTime Year { get; set; }
        public Language Language { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public Series? Series { get; set; }

        public CoverType CoverType {  get; set; }
        public int Pages { get; set; }
    }

    public enum CoverType
    {
        Paperback,
        Hardcover
    }
}
