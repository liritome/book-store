using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookBase
    {
        public int Id { get; set; }

        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public int Year { get; set; }

        public int? SeriesId { get; set; }
        public virtual Series? Series { get; set; }

        public int? NumberInSeries { get; set; }

        public virtual ICollection<BookEdition> BookEditions { get; set; } = new List<BookEdition>();
    }
}
