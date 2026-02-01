using BookStore.Services;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.ViewModels;
using BookStore.InputModels;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly ProductService productService;
        private readonly LanguageService languageService;
        private readonly SeriesService seriesService;
        private readonly PublisherService publisherService;
        private readonly DiscountService discountService;
        private readonly AuthorService authorService;
        private readonly TranslatorService translatorService;

        public BooksController(ProductService productService, LanguageService languageService,
            SeriesService seriesService, PublisherService publisherService, DiscountService discountService,
            AuthorService authorService, TranslatorService translatorService)
        {
            this.productService = productService;
            this.languageService = languageService;
            this.seriesService = seriesService;
            this.publisherService = publisherService;
            this.discountService = discountService;
            this.authorService = authorService;
            this.translatorService = translatorService;
        }

        public async Task<IActionResult> Index()
        {
            List<Book> books = await productService.GetAllBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            Book? book = await productService.GetBookByIdAsync(id);
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
            BookAddViewModel model = new BookAddViewModel
            {
                Languages = await languageService.GetAllLanguagesAsync(),
                Series = await seriesService.GetAllSeriesAsync(),
                Publishers = await publisherService.GetAllPublishersAsync(),
                Discounts = await discountService.GetAllDiscountsAsync(),
                Authors = await authorService.GetAllAuthorsAsync(),
                Translators = await translatorService.GetAllTranslatorsAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookAddInputModel model)
        {
            await productService.AddBookAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book? book = await productService.GetBookByIdAsync(id);
            if (book == null)
            {
                ViewData["ErrorMessage"] = "Book is null";
                return View("Error");
            }
            BookEditViewModel model = new BookEditViewModel
            {
                Book = book,
                Languages = await languageService.GetAllLanguagesAsync(),
                Series = await seriesService.GetAllSeriesAsync(),
                Publishers = await publisherService.GetAllPublishersAsync(),
                Discounts = await discountService.GetAllDiscountsAsync(),
                Authors = await authorService.GetAllAuthorsAsync(),
                Translators = await translatorService.GetAllTranslatorsAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookEditInputModel model)
        {
            await productService.UpdateBookAsync(model);
            return RedirectToAction("Details", new { Id = model.BookId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteBookAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleVisibility(int id)
        {
            await productService.ToggleBookVisibility(id);

            string? returnUrl = Request.Headers.Referer.ToString();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index");
        }
    }
}
