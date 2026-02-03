using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<BookBase> BookBases { get; set; } = new List<BookBase>();
    }
}
