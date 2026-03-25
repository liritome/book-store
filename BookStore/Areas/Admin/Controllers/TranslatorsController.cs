using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TranslatorsController : Controller
    {
        private readonly TranslatorService translatorService;

        public TranslatorsController(TranslatorService translatorService)
        {
            this.translatorService = translatorService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchTranslatorsJson(string query)
        {
            var translators = await translatorService.SearchTranslatorsAsync(query);
            var result = translators.Select(t => new
            {
                id = t.Id,
                name = t.Name
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SearchTranslatorByIdJson(int id)
        {
            var translator = await translatorService.GetTranslatorByIdAsync(id);
            if (translator == null)
            {
                return Json(null);
            }
            var result = new
            {
                id = translator.Id,
                name = translator.Name
            };
            return Json(result);
        }
    }
}
