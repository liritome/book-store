namespace BookStore.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Percentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
