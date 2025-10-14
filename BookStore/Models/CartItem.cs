using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}