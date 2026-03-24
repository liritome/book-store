using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookBaseEditViewModel : BookBaseAddViewModel
    {
        public BookBase BookBase { get; set; } = null!;
    }
}
