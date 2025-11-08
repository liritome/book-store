using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
