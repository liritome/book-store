using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Text { get; set; } = null!;

        [Range(0, 10)]
        public int Rating { get; set; }

        public int Likes { get; set; }
        public bool IsVisible { get; set; }
    }
}
