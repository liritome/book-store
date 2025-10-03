using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    [Owned]
    public class DigitalBookDetails
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public ICollection<string> Images { get; set; } = null!;
    }
}
