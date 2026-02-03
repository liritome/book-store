using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Series
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(120)]
        public string Slug { get; set; } = null!;

        public virtual ICollection<BookBase> BookBases { get; set; } = new List<BookBase>();
    }
}
