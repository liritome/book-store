using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookEdition
    {
        public int Id { get; set; }

        public int BookBaseId { get; set; }
        public virtual BookBase BookBase { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; } = null!;

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; } = null!;

        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; } = null!;

        public virtual ICollection<Translator> Translators { get; set; } = new List<Translator>();

        public virtual ICollection<PhysicalBook> PhysicalBooks { get; set; } = new List<PhysicalBook>();

        public virtual DigitalBook? DigitalBook { get; set; }
    }
}
