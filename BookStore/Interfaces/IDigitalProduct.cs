namespace BookStore.Interfaces
{
    public interface IDigitalProduct : IProduct
    {
        public string FileName {  get; set; }
        public string FilePath { get; set; }
    }
}
