namespace BookStore.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Book> Books { get; set; } = null!;
    }
}
