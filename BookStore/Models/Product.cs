namespace BookStore.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public ICollection<Review> Reviews { get; set; } = null!;
        public bool IsVisible { get; set; }
    }
}
