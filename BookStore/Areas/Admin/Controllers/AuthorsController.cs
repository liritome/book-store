using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly AuthorService authorService;

        public AuthorsController(AuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<JsonResult> SearchAuthorsJson(string query)
        {
            var authors = await authorService.SearchAuthorsAsync(query);
            var result = authors.Select(a => new
            {
                id = a.Id,
                name = a.Name
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> SearchAuthorByIdJson(int id)
        {
            var author = await authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return Json(null);
            }
            var result = new
            {
                id = author.Id,
                name = author.Name
            };
            return Json(result);
        }
    }
}
