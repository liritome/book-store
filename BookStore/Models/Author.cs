using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(120)]
        public string Slug { get; set; } = null!;

        [Required]
        [MaxLength(3000)]
        public string Bio { get; set; } = null!;

        public Image? Image { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
