using BookStore.InputModels;
using BookStore.Models;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhysicalBooksController : Controller
    {
        private readonly PhysicalBookService physicalBookService;
        private readonly BookEditionService bookEditionService;
        private readonly DiscountService discountService;

        public PhysicalBooksController(PhysicalBookService physicalBookService, DiscountService discountService,
                BookEditionService bookEditionService)
        {
            this.physicalBookService = physicalBookService;
            this.bookEditionService = bookEditionService;
            this.discountService = discountService;
        }

        public async Task<IActionResult> Index()
        {
            List<PhysicalBook> books = await physicalBookService.GetAllBooksAsync();
            return View(books);
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
            return RedirectToAction("Index");
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
            await physicalBookService.ReorderImagesAsync(model.ImageOrders);
            //await physicalBookService.AttachImagesToBookAsync(model.BookId, model.Images);
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
