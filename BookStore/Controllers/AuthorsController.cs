using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Authors/{slug}")]
        public IActionResult Details(string slug)
        {
            return View();
        }
    }
}
