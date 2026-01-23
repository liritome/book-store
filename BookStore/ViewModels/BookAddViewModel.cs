using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookAddViewModel
    {
        public IEnumerable<Language> Languages { get; set; } = [];
    }
}
