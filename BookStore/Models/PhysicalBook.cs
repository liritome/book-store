using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    [Owned]
    public class PhysicalBookDetails
    {
        public int Id { get; set; }
        public CoverType CoverType { get; set; }
        public int Pages { get; set; }
        public int Stock { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
        public string ISBN { get; set; } = null!;
        public ICollection<string> Images { get; set; } = null!;
    }

    public enum CoverType
    {
        Hardcover,
        Paperback
    }
}
