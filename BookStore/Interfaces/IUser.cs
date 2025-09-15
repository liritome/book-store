namespace BookStore.Interfaces
{
    public interface IUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
