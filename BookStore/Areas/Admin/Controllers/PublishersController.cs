using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublishersController : Controller
    {
        private readonly PublisherService publisherService;

        public PublishersController(PublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchPublishersJson(string query)
        {
            var publishers = await publisherService.SearchPublishersAsync(query);
            var result = publishers.Select(p => new
            {
                id = p.Id,
                name = p.Name
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SearchPublisherByIdJson(int id)
        {
            var publisher = await publisherService.GetPublisherByIdAsync(id);
            if (publisher == null)
            {
                return Json(null);
            }
            var result = new
            {
                id = publisher.Id,
                name = publisher.Name
            };
            return Json(result);
        }
    }
}
