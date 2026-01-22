using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [Owned]
    public class PhysicalBook
    {
        [Required]
        [MaxLength(120)]
        public string Slug { get; set; } = null!;

        public CoverType CoverType { get; set; }

        [Range(1, int.MaxValue)]
        public int Pages { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public bool IsAvailable { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        [Range(1, int.MaxValue)]
        public int Thickness { get; set; }

        [Range(1, int.MaxValue)]
        public int Weight { get; set; }

        [Range (1, 100000)]
        public int Price { get; set; }

        [Range(1, 100000)]
        public int FinalPrice { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }

        [Required]
        [MaxLength(100)]
        public string Isbn { get; set; } = null!;
    }

    public enum CoverType
    {
        Hardcover,
        Paperback
    }
}
