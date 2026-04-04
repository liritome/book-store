using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookAddViewModel
    {
        public IEnumerable<BookEdition> BookEditions { get; set; } = null!;
        public IEnumerable<Discount> Discounts { get; set; } = null!;
    }
}
