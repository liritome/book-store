namespace BookStore.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public DateTime ValidFor { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
