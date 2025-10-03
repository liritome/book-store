namespace BookStore.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
