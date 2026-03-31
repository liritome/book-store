using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookAddViewModel
    {
        public IEnumerable<BookEdition> BookEditions { get; set; } = new List<BookEdition>();
    }
}
