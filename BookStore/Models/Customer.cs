using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Customers")]
    public class Customer : User
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        public DateOnly BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Product> Wishlist { get; set; } = new List<Product>();

        [Range(0, int.MaxValue)]
        public int Bonuses { get; set; }
    }
}
