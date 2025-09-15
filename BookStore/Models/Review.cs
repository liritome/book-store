using BookStore.Interfaces;

namespace BookStore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public IProduct Product { get; set; }
        public int AuthorId { get; set; }
        public IUser AuthorId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public int Likes { get; set; }
        public bool IsVisible { get; set; }
    }
}
