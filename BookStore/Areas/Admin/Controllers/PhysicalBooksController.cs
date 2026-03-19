using BookStore.InputModels;
using BookStore.Models;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhysicalBooksController : Controller
    {
        private readonly PhysicalBookService physicalBookService;
        private readonly BookEditionService bookEditionService;
        private readonly DiscountService discountService;

        public PhysicalBooksController(PhysicalBookService physicalBookService,
            DiscountService discountService, BookEditionService bookEditionService)
        {
            this.physicalBookService = physicalBookService;
            this.bookEditionService = bookEditionService;
            this.discountService = discountService;
        }

        public async Task<IActionResult> Index(PhysicalBookFilter filter, PhysicalBookSortState sort = PhysicalBookSortState.IdAsc, int page = 1)
        {
            int pageSize = 10;
            var booksQuery = physicalBookService.GetAllBooksQuery(filter, sort);
            var books = await physicalBookService.GetBooksPageAsync(booksQuery, page, pageSize);
            int booksCount = booksQuery.Count();

            var model = new PhysicalBookIndexViewModel
            {
                Books = books,
                Discounts = await discountService.GetActiveAndScheduledDiscountsAsync(),
                Filter = filter,
                PageViewModel = new PageViewModel(page, booksCount, pageSize),
                Sort = sort
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            PhysicalBook? book = await physicalBookService.GetBookByIdAsync(id);
            if (book == null)
            {
                ViewData["ErrorMessage"] = "Book is null";
                return View("Error");
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            PhysicalBookAddViewModel model = new PhysicalBookAddViewModel
            {
                Discounts = await discountService.GetAllDiscountsAsync(),
                BookEditions = await bookEditionService.GetAllEditionsAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PhysicalBookAddInputModel model)
        {
            var bookEdition = await bookEditionService.GetEditionByIdAsync(model.BookEditionId);
            if (bookEdition == null)
            {
                ViewData["ErrorMessage"] = "Book edition is null";
                return View("Error");
            }
            int bookId = await physicalBookService.AddBookAsync(model, bookEdition.Title);
            await physicalBookService.AttachImagesToBookAsync(bookId, model.Images);
            return RedirectToAction("Details", new { Id = bookId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PhysicalBook? book = await physicalBookService.GetBookByIdAsync(id);
            if (book == null)
            {
                ViewData["ErrorMessage"] = "Book is null";
                return View("Error");
            }
            PhysicalBookEditViewModel model = new PhysicalBookEditViewModel
            {
                Book = book,
                Discounts = await discountService.GetAllDiscountsAsync(),
                BookEditions = await bookEditionService.GetAllEditionsAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PhysicalBookEditInputModel model)
        {
            var bookEdition = await bookEditionService.GetEditionByIdAsync(model.BookEditionId);
            if (bookEdition == null)
            {
                ViewData["ErrorMessage"] = "Book edition is null";
                return View("Error");
            }
            await physicalBookService.UpdateBookAsync(model, bookEdition.Title);
            await physicalBookService.ReorderImagesAsync(model.ImageDtos);
            await physicalBookService.AttachImagesToBookAsync(model.BookId, model.Images);
            return RedirectToAction("Details", new { Id = model.BookId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await physicalBookService.DeleteBookAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleVisibility(int id)
        {
            await physicalBookService.ToggleBookVisibilityAsync(id);

            string? returnUrl = Request.Headers.Referer.ToString();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index");
        }
    }
}
