using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class PhysicalBook : Product
    {
        public int BookEditionId { get; set; }
        public virtual BookEdition BookEdition { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Isbn { get; set; } = null!;

        [MaxLength(100)]
        public string? TitleAddition { get; set; }

        [MaxLength(3000)]
        public string? DescriptionAddition { get; set; }

        public CoverType CoverType { get; set; }

        public bool HasIllustrations { get; set; }

        [Range(1, int.MaxValue)]
        public int Pages { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        [Range(1, int.MaxValue)]
        public int Thickness { get; set; }

        [Range(1, int.MaxValue)]
        public int Weight { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public override string Name => $"{BookEdition.Title}. {BookEdition.BookBase.Series?.Name} #{BookEdition.BookBase.NumberInSeries}. {TitleAddition}";

        public override string Description => $"{DescriptionAddition}\n\n{BookEdition.Description}";
    }

    public enum CoverType
    {
        Hardcover,
        Paperback
    }

    public enum PhysicalBookSortState
    {
        IdAsc,
        IdDesc,
        TitleAsc,
        TitleDesc,
        CoverAsc,
        CoverDesc,
        IsbnAsc,
        IsbnDesc,
        PriceAsc,
        PriceDesc,
        DiscountAsc,
        DiscountDesc,
        FinalPriceAsc,
        FinalPriceDesc,
        StockAsc,
        StockDesc
    }
}
