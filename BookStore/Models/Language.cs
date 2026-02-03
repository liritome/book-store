using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<BookEdition> BookEditions { get; set; } = new List<BookEdition>();
    }
}
