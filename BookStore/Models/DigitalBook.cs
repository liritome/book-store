using BookStore.Interfaces;

namespace BookStore.Models
{
    public class DigitalBook : IDigitalProduct, IBook
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

        // IDigitalProduct
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

        // IBook
        public Author Author { get; set; } = null!;
        public Translator? Translator { get; set; } = null!;
        public string ISBN { get; set; } = string.Empty;
        public Publisher Publisher { get; set; } = null!;
        public DateTime Year { get; set; }
        public Language Language { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public Series? Series { get; set; }
    }
}
