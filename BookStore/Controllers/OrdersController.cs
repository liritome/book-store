using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Orders/{id}")]
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
