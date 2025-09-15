using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Discount? Discount { get; set; }
        public double Rating { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<string> Images { get; set; }
        public bool IsVisible { get; set; }
        public int GetTotalPrice()
        {
            decimal totalPrice = Discount == null ? Price : Price * (1 - Discount.Percentage / 100);
            return (int)Math.Round(totalPrice, MidpointRounding.AwayFromZero);
        }
    }
}
