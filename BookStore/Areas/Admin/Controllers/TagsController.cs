using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private readonly TagService tagService;

        public TagsController(TagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchTagsJson(string query)
        {
            var tags = await tagService.SearchTagsAsync(query);
            var result = tags.Select(t => new
            {
                id = t.Id,
                name = t.Name
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SearchTagByIdJson(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);
            if (tag == null)
            {
                return Json(null);
            }
            var result = new
            {
                id = tag.Id,
                name = tag.Name
            };
            return Json(result);
        }
    }
}
