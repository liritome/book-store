using BookStore.Models;

namespace BookStore.InputModels
{
    public class BookBaseFilter
    {
        public string? Query { get; set; }
        public List<int> AuthorIds { get; set; } = new();
        public List<int> TagIds { get; set; } = new();
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public int? EditionsCountFrom { get; set; }
        public int? EditionsCountTo { get; set; }
    }
}
