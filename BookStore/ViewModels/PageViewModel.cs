namespace BookStore.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems {  get; set; }
        public bool HasPrevPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int GetLowerPageRange => (PageNumber - 1) * PageSize + 1;
        public int GetUpperPageRange => Math.Min(PageNumber * PageSize, TotalItems);

        public PageViewModel(int pageNumber, int itemsCount, int pageSize)
        {
            PageNumber = pageNumber;
            TotalItems = itemsCount;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(itemsCount / (double)pageSize);
        }
    }
}
