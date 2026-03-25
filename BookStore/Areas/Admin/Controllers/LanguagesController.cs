using Microsoft.AspNetCore.Mvc;
using BookStore.Services;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguagesController : Controller
    {
        private readonly LanguageService languageService;

        public LanguagesController(LanguageService languageService)
        {
            this.languageService = languageService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchLanguagesJson(string query)
        {
            var languages = await languageService.SearchLanguagesAsync(query);
            var result = languages.Select(l => new
            {
                id = l.Id,
                name = l.Name
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SearchLanguageByIdJson(int id)
        {
            var language = await languageService.GetLanguageByIdAsync(id);
            if (language == null)
            {
                return Json(null);
            }
            var result = new
            {
                id = language.Id,
                name = language.Name
            };
            return Json(result);
        }
    }
}
