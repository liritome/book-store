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

        public BookBasesController(BookBaseService bookBaseService)
        {
            this.bookBaseService = bookBaseService;
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
    }
}
