using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class TranslatorsController : Controller
    {
        [Route("Translators/{slug}")]
        public IActionResult Details(string slug)
        {
            return View();
        }
    }
}
