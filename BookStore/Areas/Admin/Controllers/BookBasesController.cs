using BookStore.InputModels;
using BookStore.Models;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookBasesController : Controller
    {
        private readonly BookBaseService bookBaseService;
        private readonly AuthorService authorService;
        private readonly TagService tagService;
        private readonly SeriesService seriesService;

        public BookBasesController(BookBaseService bookBaseService, AuthorService authorService,
            TagService tagService, SeriesService seriesService)
        {
            this.bookBaseService = bookBaseService;
            this.authorService = authorService;
            this.tagService = tagService;
            this.seriesService = seriesService;
        }

        public async Task<IActionResult> Index(BookBaseFilter filter, BookBaseSortState sort = BookBaseSortState.IdAsc, int page = 1)
        {
            int pageSize = 10;
            var bookBasesQuery = bookBaseService.GetAllBookBasesQuery(filter, sort);
            var bookBases = await bookBaseService.GetBookBasesPageAsync(bookBasesQuery, page, pageSize);
            int bookBasesCount = bookBasesQuery.Count();

            var model = new BookBaseIndexViewModel
            {
                BookBases = bookBases,
                Filter = filter,
                Sort = sort,
                PageViewModel = new PageViewModel(page, bookBasesCount, pageSize)
            };
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            BookBase? bookBase = await bookBaseService.GetBookBaseByIdAsync(id);
            if(bookBase == null)
            {
                ViewData["ErrorMessage"] = "BookBase is null";
                return View("Error");
            }
            return View(bookBase);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            BookBaseAddViewModel model = new BookBaseAddViewModel
            {
                Authors = await authorService.GetAllAuthorsAsync(),
                Series = await seriesService.GetAllSeriesAsync(),
                Tags = await tagService.GetAllTagsAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookBaseAddInputModel model)
        {
            var bookBaseId = await bookBaseService.AddBookBaseAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await bookBaseService.DeleteBookBaseAsync(id);

            string? returnUrl = Request.Headers.Referer.ToString();
            if (!string.IsNullOrEmpty(returnUrl) && !returnUrl.Contains("Details"))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index");
        }
    }
}
