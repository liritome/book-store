using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Required]
        [Url]
        public string Url { get; set; } = null!;
        public int Number { get; set; }

        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
