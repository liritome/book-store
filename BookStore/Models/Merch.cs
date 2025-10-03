using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("MerchItems")]
    public class Merch : Product
    {
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
        public ICollection<string> Images { get; set; } = null!;
    }
}
