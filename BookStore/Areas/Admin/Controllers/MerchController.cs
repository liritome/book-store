using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MerchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
