using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookAddViewModel
    {
        public IEnumerable<Discount> Discounts { get; set; } = new List<Discount>();
        public IEnumerable<BookEdition> BookEditions { get; set; } = new List<BookEdition>();
    }
}
