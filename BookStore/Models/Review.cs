namespace BookStore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
        public int Likes { get; set; }
        public bool IsVisible { get; set; }
    }
}
