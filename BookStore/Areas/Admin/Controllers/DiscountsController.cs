using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountsController : Controller
    {
        private DiscountService discountService;

        public DiscountsController(DiscountService discountService)
        {
            this.discountService = discountService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchDiscountsJson(string query)
        {
            var discounts = await discountService.SearchDiscountsAsync(query);
            var result = discounts.Select(d => new
            {
                id = d.Id,
                name = $"[{d.Display()}] {d.Name} ({d.ValidFrom} - {d.ValidTo})"
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SearchDiscountByIdJson(int id)
        {
            var discount = await discountService.GetDiscountByIdAsync(id);
            if(discount == null)
            {
                return Json(null);
            }
            var result = new
            {
                id = discount.Id,
                name = $"[{discount.Display()}] {discount.Name} ({discount.ValidFrom} - {discount.ValidTo})"
            };
            return Json(result);
        }
    }
}
