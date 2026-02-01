using BookStore.Models;

namespace BookStore.InputModels
{
    public class BookAddInputModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string IsVisible { get; set; } = null!;
        public int? AuthorId { get; set; }
        public int? TranslatorId { get; set; }
        public int? PublisherId { get; set; }
        public int Year { get; set; }
        public int LanguageId { get; set; }
        public int? SeriesId { get; set; }
        public int? NumberInSeries { get; set; }
        public bool HasDigitalBook { get; set; }

        public string Slug { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public CoverType CoverType { get; set; }
        public int Pages { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Thickness { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public int? DiscountId { get; set; }
        public int FinalPrice { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }

        public string DigitalSlug { get; set; } = null!;
        public string DigitalIsbn { get; set; } = null!;
        public IFormFile? File { get; set; }
        public int DigitalPrice { get; set; }
        public int? DigitalDiscountId { get; set; }
        public int DigitalFinalPrice { get; set; }
        public bool DigitalIsAvailable { get; set; }
    }
}
