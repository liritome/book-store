using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Books")]
    public class Book : Product
    {
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

        public virtual ICollection<Translator> Translators { get; set; } = new List<Translator>();

        public int? PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; } = null!;

        public int Year { get; set; }

        public int? LanguageId { get; set; }
        public virtual Language? Language { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public int? SeriesId { get; set; }
        public virtual Series? Series { get; set; }

        public int? NumberInSeries { get; set; }

        public DigitalBook? DigitalBook { get; set; }
        public PhysicalBook? PhysicalBook { get; set; } = null!;
    }
}
