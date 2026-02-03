using BookStore.Models;

namespace BookStore.ViewModels
{
    public class PhysicalBookEditViewModel : PhysicalBookAddViewModel
    {
        public PhysicalBook Book { get; set; } = null!;
    }
}
