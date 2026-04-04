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
        private readonly SeriesService seriesService;
        private readonly AuthorService authorService;
        private readonly TagService tagService;

        public BookBasesController(BookBaseService bookBaseService, SeriesService seriesService,
            AuthorService authorService, TagService tagService)
        {
            this.bookBaseService = bookBaseService;
            this.seriesService = seriesService;
            this.authorService = authorService;
            this.tagService = tagService;
        }

        public async Task<IActionResult> Index(BookBaseFilter filter,
            BookBaseSortState sort = BookBaseSortState.IdAsc, int page = 1)
        {
            int pageSize = 10;
            var bookBasesQuery = bookBaseService.GetAllBookBasesQuery(filter, sort);
            var bookBases = await bookBaseService.GetBookBasesPageAsync(bookBasesQuery, page, pageSize);
            int bookBasesCount = bookBasesQuery.Count();

            var model = new BookBaseIndexViewModel
            {
                BookBases = bookBases,
                Authors = bookBases
                    .SelectMany(bb => bb.Authors)
                    .DistinctBy(b => b.Id)
                    .ToList(),
                Tags = bookBases
                    .SelectMany(bb => bb.Tags)
                    .DistinctBy(t => t.Id)
                    .ToList(),
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
                Series = await seriesService.GetAllSeriesAsync(),
                Authors = await authorService.GetAllAuthorsAsync(),
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookBase? bookBase = await bookBaseService.GetBookBaseByIdAsync(id);
            if(bookBase == null)
            {
                ViewData["ErrorMessage"] = "BookBase is null";
                return View("Error");
            }
            BookBaseEditViewModel model = new BookBaseEditViewModel
            {
                BookBase = bookBase
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookBaseEditInputModel model)
        {
            await bookBaseService.UpdateBookBaseAsync(model);
            return RedirectToAction("Details", new { Id = model.BookBaseId });
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
