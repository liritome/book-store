using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookEditViewModel : BookAddViewModel
    {
        public Book Book { get; set; } = null!;
    }
}
