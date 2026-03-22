using BookStore.InputModels;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PaginationViewModel
    {
        public PageViewModel PageViewModel { get; set; } = null!;
        public object Filter { get; set; } = new();
        public Enum Sort { get; set; } = null!;
        public string Area { get; set; } = "";
        public string Controller { get; set; } = "";
        public string Action { get; set; } = "";
    }
}
