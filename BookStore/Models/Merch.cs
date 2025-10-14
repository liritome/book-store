using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace BookStore.Models
{
    [Table("MerchItems")]
    public class Merch : Product
    {
        [Range(1, 100000)]
        public decimal Price { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
