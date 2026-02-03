using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookAddViewModel
    {
        public IEnumerable<Discount> Discounts { get; set; } = [];
        public IEnumerable<BookEdition> BookEditions { get; set; } = [];
    }
}
