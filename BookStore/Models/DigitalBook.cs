using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class DigitalBook : Product
    {
        public int BookEditionId { get; set; }
        public virtual BookEdition BookEdition { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Isbn { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FileName { get; set; } = null!;

        public override string Name => $"{BookEdition.Title}. {BookEdition.BookBase.Title} #{BookEdition.BookBase.NumberInSeries}. E-book";

        public override string Description => $"Digital book.\n\n{BookEdition.Description}";
    }
}
