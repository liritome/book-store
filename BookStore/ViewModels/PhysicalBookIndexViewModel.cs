using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookIndexViewModel
    {
        public IEnumerable<PhysicalBook> Books { get; set; } = null!;

        public PhysicalBookSortState CurrentSort { get; set; }

        public PhysicalBookSortState IdSort { get; set; }
        public PhysicalBookSortState TitleSort { get; set; }
        public PhysicalBookSortState CoverSort { get; set; }
        public PhysicalBookSortState IsbnSort { get; set; }
        public PhysicalBookSortState PriceSort { get; set; }
        public PhysicalBookSortState DiscountSort { get; set; }
        public PhysicalBookSortState FinalPriceSort { get; set; }
        public PhysicalBookSortState StockSort { get; set; }
    }
}
