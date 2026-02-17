using BookStore.InputModels;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookIndexViewModel
    {
        public IEnumerable<PhysicalBook> Books { get; set; } = null!;
        public IEnumerable<Author> Authors { get; set; } = null!;
        public IEnumerable<Publisher> Publishers { get; set; } = null!;
        public IEnumerable<Translator> Translators { get; set; } = null!;
        public IEnumerable<Language> Languages { get; set; } = null!;
        public IEnumerable<Discount> Discounts { get; set; } = null!;

        public PhysicalBookIndexInputModel Input { get; set; } = new();

        public PhysicalBookSortState IdSort => Input.Sort == PhysicalBookSortState.IdAsc
            ? PhysicalBookSortState.IdDesc : PhysicalBookSortState.IdAsc;

        public PhysicalBookSortState TitleSort => Input.Sort == PhysicalBookSortState.TitleAsc
            ? PhysicalBookSortState.TitleDesc : PhysicalBookSortState.TitleAsc;

        public PhysicalBookSortState CoverSort => Input.Sort == PhysicalBookSortState.CoverAsc
            ? PhysicalBookSortState.CoverDesc : PhysicalBookSortState.CoverAsc;

        public PhysicalBookSortState IsbnSort => Input.Sort == PhysicalBookSortState.IsbnAsc 
            ? PhysicalBookSortState.IsbnDesc : PhysicalBookSortState.IsbnAsc;

        public PhysicalBookSortState PriceSort => Input.Sort == PhysicalBookSortState.PriceAsc
            ? PhysicalBookSortState.PriceDesc : PhysicalBookSortState.PriceAsc;

        public PhysicalBookSortState DiscountSort => Input.Sort == PhysicalBookSortState.DiscountAsc
            ? PhysicalBookSortState.DiscountDesc : PhysicalBookSortState.DiscountAsc;

        public PhysicalBookSortState FinalPriceSort => Input.Sort == PhysicalBookSortState.FinalPriceAsc
            ? PhysicalBookSortState.FinalPriceDesc : PhysicalBookSortState.FinalPriceAsc;

        public PhysicalBookSortState StockSort => Input.Sort == PhysicalBookSortState.StockAsc
            ? PhysicalBookSortState.StockDesc : PhysicalBookSortState.StockAsc;

    }
}
