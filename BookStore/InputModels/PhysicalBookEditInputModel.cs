namespace BookStore.InputModels
{
    public class PhysicalBookEditInputModel : PhysicalBookAddInputModel
    {
        public int BookId { get; set; }
        public List<ImageDto> ImageDtos { get; set; } = new();
    }

    public class ImageDto
    {
        public int ImageId { get; set; }
        public int Order {  get; set; }
        public bool Delete { get; set; }
    }
}
