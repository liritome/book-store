using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Books")]
    public class Book : Product
    {
        public ICollection<Author> Authors { get; set; } = null!;
        public ICollection<Translator> Translators { get; set; } = null!;
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = null!;
        public DateTime Year { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
        public ICollection<Tag> Tags { get; set; } = null!;
        public int? SeriesId { get; set; }
        public Series? Series { get; set; }
        public DigitalBookDetails? DigitalBookDetails { get; set; }
        public PhysicalBookDetails PhysicalBookDetails { get; set; } = null!;
    }
}
