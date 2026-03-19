using BookStore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author : ISlug
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

        public string? ImageUrl { get; set; }

        public virtual ICollection<BookBase> BookBases { get; set; } = new List<BookBase>();
    }
}
