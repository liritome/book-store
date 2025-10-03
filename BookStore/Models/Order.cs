namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = null!;
    }

    public enum OrderStatus
    {
        New,
        Processing,
        Sent,
        Delivered,
        Completed,
        Cancelled,
        Refunded
    }
}