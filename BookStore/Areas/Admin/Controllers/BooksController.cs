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

        public BooksController(ProductService productService, LanguageService languageService, SeriesService seriesService)
        {
            this.productService = productService;
            this.languageService = languageService;
            this.seriesService = seriesService;
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
                Series = await seriesService.GetAllSeriesAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookAddInputModel model)
        {
            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                Rating = 0,
                Reviews = new List<Review>(),
                Images = new List<ProductImage>(),
                IsVisible = model.IsVisible switch
                {
                    "Visible" => true,
                    "Hidden" => false,
                    "Scheduled" => false,
                    _ => false
                },

                Authors = new List<Author>(),
                Translators = new List<Translator>(),
                //PublisherId = model.PublisherId,
                Year = model.Year,
                LanguageId = model.LanguageId,
                Tags = new List<Tag>(),
                SeriesId = model.SeriesId,
                NumberInSeries = model.NumberInSeries,
                PhysicalBook = new PhysicalBook
                {
                    Slug = model.Slug,
                    CoverType = model.CoverType,
                    Pages = model.Pages,
                    Stock = model.Stock,
                    IsAvailable = model.IsAvailable,
                    Width = model.Width,
                    Height = model.Height,
                    Thickness = model.Thickness,
                    Weight = model.Weight,
                    Price = model.Price,
                    FinalPrice = model.FinalPrice,
                    //DiscountId = model.DigitalDiscountId,
                    Isbn = model.Isbn
                },
                DigitalBook = model.HasDigitalBook ? new DigitalBook
                {
                    Slug = model.DigitalSlug,
                    FileName = model.File == null ? "" : model.File.FileName,
                    IsAvailable = model.DigitalIsAvailable,
                    Price = model.DigitalPrice,
                    FinalPrice = model.DigitalFinalPrice,
                    //DiscountId = model.DiscountId,
                    Isbn = model.DigitalIsbn
                } : null
            };

            await productService.AddBookAsync(book);
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
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookEditInputModel model)
        {
            Book? book = await productService.GetBookByIdAsync(model.Id);
            if(book == null)
            {
                ViewData["ErrorMessage"] = "Book is null";
                return View("Error");
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.Images = new List<ProductImage>();
            book.IsVisible = model.IsVisible switch
            {
                "Visible" => true,
                "Hidden" => false,
                "Scheduled" => false,
                _ => false
            };
            book.Authors = new List<Author>();
            book.Translators = new List<Translator>();
            // book.PublisherId = model.PublisherId;
            book.Year = model.Year;
            book.LanguageId = model.LanguageId;
            book.Tags = new List<Tag>();
            book.SeriesId = model.SeriesId;
            book.NumberInSeries = model.NumberInSeries;

            book.PhysicalBook ??= new PhysicalBook();
            book.PhysicalBook.Slug = model.Slug;
            book.PhysicalBook.CoverType = model.CoverType;
            book.PhysicalBook.Pages = model.Pages;
            book.PhysicalBook.Stock = model.Stock;
            book.PhysicalBook.IsAvailable = model.IsAvailable;
            book.PhysicalBook.Width = model.Width;
            book.PhysicalBook.Height = model.Height;
            book.PhysicalBook.Thickness = model.Thickness;
            book.PhysicalBook.Weight = model.Weight;
            book.PhysicalBook.Price = model.Price;
            book.PhysicalBook.FinalPrice = model.FinalPrice;
            // book.PhysicalBook.DiscountId = model.DiscountId;
            book.PhysicalBook.Isbn = model.Isbn;

            if(!model.HasDigitalBook)
            {
                book.DigitalBook = null;
            }
            else
            {
                book.DigitalBook ??= new DigitalBook();
                book.DigitalBook.Slug = model.DigitalSlug;
                book.DigitalBook.FileName = model.File == null ? "" : model.File.FileName;
                book.DigitalBook.IsAvailable = model.DigitalIsAvailable;
                book.DigitalBook.Price = model.DigitalPrice;
                book.DigitalBook.FinalPrice = model.DigitalFinalPrice;
                // book.DigitalBook.DiscountId = model.DigitalDiscountId;
                book.DigitalBook.Isbn = model.DigitalIsbn;
            }

            await productService.UpdateBookAsync(book);
            return RedirectToAction("Details", new { book.Id });
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
