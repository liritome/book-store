using BookStore.Models;

namespace BookStore.InputModels
{
    public class PhysicalBookAddInputModel
    {
        public int BookEditionId { get; set; }
        public string Isbn { get; set; } = null!;
        public string TitleAddition { get; set; } = null!;
        public string DescriptionAddition { get; set; } = null!;
        public CoverType CoverType { get; set; }
        public bool HasIllustrations { get; set; }
        public int Pages { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public int Weight { get; set; }
        public int Stock { get; set; }

        public string Slug { get; set; } = null!;
        public int Price { get; set; }
        public int? DiscountId { get; set; }
        public int FinalPrice { get; set; }
        public string IsVisible { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
