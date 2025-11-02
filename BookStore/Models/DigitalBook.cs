using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [Owned]
    public class DigitalBookDetails
    {
        [Required]
        [MaxLength(120)]
        public string Slug { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FileName { get; set; } = null!;

        [Required]
        public string FilePath { get; set; } = null!;

        [Range(1, 100000)]
        public decimal Price { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }

        [Required]
        [MaxLength(100)]
        public string ISBN { get; set; } = null!;

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
