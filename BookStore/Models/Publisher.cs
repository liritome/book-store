using BookStore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Publisher : ISlug
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(120)]
        public string Slug { get; set; } = null!;

        public string? ImageUrl { get; set; }
        public virtual ICollection<BookEdition> BookEditions { get; set; } = new List<BookEdition>();
    }
}
