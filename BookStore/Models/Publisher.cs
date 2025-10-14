using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public Image? Image { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
