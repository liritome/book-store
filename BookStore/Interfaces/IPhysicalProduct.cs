namespace BookStore.Interfaces
{
    public interface IPhysicalProduct : IProduct
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Stock { get; set; }
        public string GetSize();
        public bool IsInStock();
    }
}
