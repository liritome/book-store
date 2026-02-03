using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Translator
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public virtual ICollection<BookEdition> BookEditions { get; set; } = new List<BookEdition>();
    }
}
