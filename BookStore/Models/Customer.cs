using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Customers")]
    public class Customer : User
    {
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        public ICollection<Product> Wishlist { get; set; } = null!;
        public int Bonuses { get; set; }
    }
}
