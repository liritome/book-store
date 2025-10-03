namespace BookStore.Models
{
    public class AdminAction
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public DateTime Date { get; set; }
        public AdminPermission Action { get; set; }
        public string Details { get; set; } = null!;
    }
}
