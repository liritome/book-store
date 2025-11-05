using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PublishersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Publishers/{slug}")]
        public IActionResult Details(string slug)
        {
            return View();
        }
    }
}
