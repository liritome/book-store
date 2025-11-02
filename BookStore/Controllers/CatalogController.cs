using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Catalog/{slug}")]
        public IActionResult Details(string slug)
        {
            return View();
        }
    }
}
