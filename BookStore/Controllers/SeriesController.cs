using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class SeriesController : Controller
    {
        [Route("Series/{slug}")]
        public IActionResult Details(string slug)
        {
            return View();
        }
    }
}
