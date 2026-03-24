using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookBaseAddViewModel
    {
        public IEnumerable<Author> Authors { get; set; } = new List<Author>();
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
        public IEnumerable<Series> Series { get; set; } = new List<Series>();
    }
}
