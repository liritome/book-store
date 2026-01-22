using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public int ProductId {  get; set; }
        public virtual Product Product { get; set; } = null!;

        [Range(1, 100000)]
        public int Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}