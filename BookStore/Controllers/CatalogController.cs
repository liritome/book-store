using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }
    }
}
