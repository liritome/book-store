using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public abstract class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; } = null!;

        [Range(0, 10)]
        public double Rating { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public bool IsVisible { get; set; }
    }
}
