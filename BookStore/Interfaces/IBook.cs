using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBook
    {
        public Author Author { get; set; }
        public Translator? Translator { get; set; }
        public string ISBN {  get; set; }
        public Publisher Publisher { get; set; }
        public DateTime Year { get; set; }
        public Language Language { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public Series? Series { get; set; }
    }

    public enum Language
    {
        English,
        Ukrainian
    }
}
