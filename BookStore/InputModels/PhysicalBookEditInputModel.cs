namespace BookStore.InputModels
{
    public class PhysicalBookEditInputModel : PhysicalBookAddInputModel
    {
        public int BookId { get; set; }
        public List<ImageOrder> ImageOrders { get; set; } = new();
    }

    public class ImageOrder
    {
        public int ImageId { get; set; }
        public int Order {  get; set; }
    }
}
