using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public abstract class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Slug { get; set; } = null!;

        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [Range(1, 100000)]
        public int Price { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount? Discount { get; set; }

        [Range(1, 100000)]
        public int FinalPrice { get; set; }

        public bool IsVisible { get; set; }

        public bool IsAvailable { get; set; }

        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

        [NotMapped]
        public double? Rating {
            get
            {
                if (Reviews.Count == 0)
                {
                    return null;
                }
                return Math.Round(Reviews.Average(r => r.Rating), 1);
            }
        }

        [NotMapped]
        public abstract string Name { get; }

        [NotMapped]
        public abstract string Description { get; }
    }
}
