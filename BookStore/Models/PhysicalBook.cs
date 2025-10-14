using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [Owned]
    public class PhysicalBookDetails
    {
        public int Id { get; set; }
        public CoverType CoverType { get; set; }

        [Range(1, int.MaxValue)]
        public int Pages { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Length { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        [Range(1, int.MaxValue)]
        public int Weight { get; set; }

        [Range (1, 100000)]
        public decimal Price { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }

        [Required]
        [MaxLength(100)]
        public string ISBN { get; set; } = null!;

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }

    public enum CoverType
    {
        Hardcover,
        Paperback
    }
}
