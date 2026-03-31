namespace BookStore.InputModels
{
    public class BookBaseAddInputModel
    {
        public int? SeriesId { get; set; }
        public int? NumberInSeries { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public List<int> AuthorIds { get; set; } = new();
        public List<int> TagIds { get; set; } = new();
    }
}
