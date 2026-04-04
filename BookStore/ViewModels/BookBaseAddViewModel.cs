using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookBaseAddViewModel
    {
        public IEnumerable<Author> Authors { get; set; } = null!;
        public IEnumerable<Series> Series { get; set; } = null!;
        public IEnumerable<Tag> Tags { get; set; } = null!;
    }
}
