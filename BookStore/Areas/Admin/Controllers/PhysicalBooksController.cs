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
        private readonly AuthorService authorService;
        private readonly TranslatorService translatorService;
        private readonly PublisherService publisherService;
        private readonly LanguageService languageService;

        public PhysicalBooksController(PhysicalBookService physicalBookService, DiscountService discountService,
                BookEditionService bookEditionService, AuthorService authorService, TranslatorService translatorService,
                PublisherService publisherService, LanguageService languageService)
        {
            this.physicalBookService = physicalBookService;
            this.bookEditionService = bookEditionService;
            this.discountService = discountService;
            this.authorService = authorService;
            this.translatorService = translatorService;
            this.publisherService = publisherService;
            this.languageService = languageService;
        }

        public async Task<IActionResult> Index(PhysicalBookFilter filter, PhysicalBookSortState sort = PhysicalBookSortState.IdAsc, int page = 1)
        {
            int pageSize = 1;
            var booksQuery = physicalBookService.GetAllBooksQuery(filter, sort);
            var books = await physicalBookService.GetBooksPageAsync(booksQuery, page, pageSize);
            int booksCount = booksQuery.Count();

            var model = new PhysicalBookIndexViewModel
            {
                Books = books,
                Discounts = await discountService.GetActiveAndScheduledDiscountsAsync(),
                Filter = filter,
                PageViewMode = new PageViewModel(page, booksCount, pageSize),
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
            int bookId = await physicalBookService.AddBookAsync(model);
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
            await physicalBookService.UpdateBookAsync(model);
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
