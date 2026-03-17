using BookStore.InputModels;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PaginationViewModel
    {
        public PageViewModel PageViewModel { get; set; } = null!;
        public PhysicalBookFilter Filter { get; set; } = new();
        public PhysicalBookSortState Sort { get; set; }
        public string Area { get; set; } = "";
        public string Controller { get; set; } = "";
        public string Action { get; set; } = "";
    }
}
