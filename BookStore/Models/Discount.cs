using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Discount
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Range(1, 100)]
        public int Percentage { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
