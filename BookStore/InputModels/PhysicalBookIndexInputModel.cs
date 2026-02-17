using BookStore.Models;

namespace BookStore.InputModels
{
    public class PhysicalBookIndexInputModel
    {
        public PhysicalBookSortState Sort { get; set; } = PhysicalBookSortState.IdAsc;
        public string? Query { get; set; }
        public int? AuthorId { get; set; }
        public int? PublisherId { get; set; }
        public int? TranslatorId { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public int? PagesFrom { get; set; }
        public int? PagesTo { get; set; }
        public int? StockFrom { get; set; }
        public int? StockTo { get; set; }
        public int? LanguageId { get; set; }
        public List<string> Visibility { get; set; } = new();
        public List<CoverType> CoverTypes { get; set; } = new();
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public int? FinalFrom { get; set; }
        public int? FinalTo { get; set; }
        public int? DiscountId { get; set; }
    }
}
