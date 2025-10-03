namespace BookStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = null!;
    }
}