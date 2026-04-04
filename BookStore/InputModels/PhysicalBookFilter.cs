using BookStore.Models;

namespace BookStore.InputModels
{
    public class PhysicalBookFilter
    {
        public string? Query { get; set; }
        public List<int> AuthorIds { get; set; } = new();
        public List<int> PublisherIds { get; set; } = new();
        public List<int> TranslatorIds { get; set; } = new();
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public int? PagesFrom { get; set; }
        public int? PagesTo { get; set; }
        public int? StockFrom { get; set; }
        public int? StockTo { get; set; }
        public List<int> TagIds { get; set; } = new();
        public List<int> LanguageIds { get; set; } = new();
        public List<string> Visibility { get; set; } = new();
        public List<CoverType> CoverTypes { get; set; } = new();
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public List<int> DiscountIds { get; set; } = new();
        public int? FinalFrom { get; set; }
        public int? FinalTo { get; set; }
    }
}
