using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(50)]
        public string RecipientFullName { get; set; } = null!;

        [Required]
        [Phone]
        public string RecipientPhoneNumber { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public int BonusesUsed { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = null!;

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
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

    public enum PaymentMethod
    {
        CreditCard,
        Cash
    }
}