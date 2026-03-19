using BookStore.InputModels;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookIndexViewModel
    {
        public IEnumerable<PhysicalBook> Books { get; set; } = null!;
        public IEnumerable<Discount> Discounts { get; set; } = null!;

        public PhysicalBookFilter Filter { get; set; } = new();

        public PageViewModel PageViewModel { get; set; } = null!;

        public PhysicalBookSortState Sort { get; set; }

        public PhysicalBookSortState IdSort => Sort == PhysicalBookSortState.IdAsc
            ? PhysicalBookSortState.IdDesc : PhysicalBookSortState.IdAsc;

        public PhysicalBookSortState TitleSort => Sort == PhysicalBookSortState.TitleAsc
            ? PhysicalBookSortState.TitleDesc : PhysicalBookSortState.TitleAsc;

        public PhysicalBookSortState CoverSort => Sort == PhysicalBookSortState.CoverAsc
            ? PhysicalBookSortState.CoverDesc : PhysicalBookSortState.CoverAsc;

        public PhysicalBookSortState IsbnSort => Sort == PhysicalBookSortState.IsbnAsc
            ? PhysicalBookSortState.IsbnDesc : PhysicalBookSortState.IsbnAsc;

        public PhysicalBookSortState PriceSort => Sort == PhysicalBookSortState.PriceAsc
            ? PhysicalBookSortState.PriceDesc : PhysicalBookSortState.PriceAsc;

        public PhysicalBookSortState DiscountSort => Sort == PhysicalBookSortState.DiscountAsc
            ? PhysicalBookSortState.DiscountDesc : PhysicalBookSortState.DiscountAsc;

        public PhysicalBookSortState FinalPriceSort => Sort == PhysicalBookSortState.FinalPriceAsc
            ? PhysicalBookSortState.FinalPriceDesc : PhysicalBookSortState.FinalPriceAsc;

        public PhysicalBookSortState StockSort => Sort == PhysicalBookSortState.StockAsc
            ? PhysicalBookSortState.StockDesc : PhysicalBookSortState.StockAsc;

    }
}
