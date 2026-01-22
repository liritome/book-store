using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [Owned]
    public class DigitalBook
    {
        [Required]
        [MaxLength(120)]
        public string Slug { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FileName { get; set; } = null!;

        public bool IsAvailable { get; set; }

        [Range(1, 100000)]
        public int Price { get; set; }

        [Range(1, 100000)]
        public int FinalPrice { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }

        [Required]
        [MaxLength(100)]
        public string Isbn { get; set; } = null!;
    }
}
