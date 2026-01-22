using BookStore.Services;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly ProductService productService;
        public BooksController(ProductService productService)
        {
            this.productService = productService;
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookAddViewModel bookModel)
        {
            var book = new Book
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
                Rating = 0,
                Reviews = new List<Review>(),
                Images = new List<ProductImage>(),
                IsVisible = bookModel.IsVisible switch
                {
                    "Visible" => true,
                    "Hidden" => false,
                    _ => false
                },

                Authors = new List<Author>(),
                Translators = new List<Translator>(),
                //Publisher = new Publisher(),
                Year = bookModel.Year,
                //Language = new Language(),
                Tags = new List<Tag>(),
                //Series = new Series(),
                NumberInSeries = bookModel.NumberInSeries,
                DigitalBook = new DigitalBook
                {
                    Slug = bookModel.DigitalSlug,
                    FileName = bookModel.File == null ? "" : bookModel.File.FileName,
                    IsAvailable = bookModel.DigitalIsAvailable,
                    Price = bookModel.DigitalPrice,
                    FinalPrice = bookModel.DigitalFinalPrice,
                    //Discount = new Discount(),
                    Isbn = bookModel.DigitalIsbn
                },
                PhysicalBook = new PhysicalBook
                {
                    Slug = bookModel.Slug,
                    CoverType = bookModel.CoverType,
                    Pages = bookModel.Pages,
                    Stock = bookModel.Stock,
                    IsAvailable = bookModel.IsAvailable,
                    Width = bookModel.Width,
                    Height = bookModel.Height,
                    Thickness = bookModel.Thickness,
                    Weight = bookModel.Weight,
                    Price = bookModel.Price,
                    FinalPrice = bookModel.FinalPrice,
                    //Discount = new Discount(),
                    Isbn = bookModel.Isbn
                }
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
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel bookModel)
        {
            Book? book = await productService.GetBookByIdAsync(bookModel.Id);
            if(book == null)
            {
                ViewData["ErrorMessage"] = "Book is null";
                return View("Error");
            }

            book.Title = bookModel.Title;
            book.Description = bookModel.Description;
            book.Images = new List<ProductImage>();
            book.IsVisible = bookModel.IsVisible switch
            {
                "Visible" => true,
                "Hidden" => false,
                _ => false
            };
            book.Authors = new List<Author>();
            book.Translators = new List<Translator>();
            // book.Publisher = new Publisher();
            book.Year = bookModel.Year;
            // book.Language = new Language();
            book.Tags = new List<Tag>();
            // book.Series = new Series();
            book.NumberInSeries = bookModel.NumberInSeries;

            if (book.DigitalBook == null)
            {
                book.DigitalBook = new DigitalBook();
            }
            book.DigitalBook.Slug = bookModel.DigitalSlug;
            book.DigitalBook.FileName = bookModel.File == null ? "" : bookModel.File.FileName;
            book.DigitalBook.IsAvailable = bookModel.DigitalIsAvailable;
            book.DigitalBook.Price = bookModel.DigitalPrice;
            book.DigitalBook.FinalPrice = bookModel.DigitalFinalPrice;
            // book.DigitalBook.Discount = new Discount();
            book.DigitalBook.Isbn = bookModel.DigitalIsbn;

            if (book.PhysicalBook == null)
            {
                book.PhysicalBook = new PhysicalBook();
            }
            book.PhysicalBook.Slug = bookModel.Slug;
            book.PhysicalBook.CoverType = bookModel.CoverType;
            book.PhysicalBook.Pages = bookModel.Pages;
            book.PhysicalBook.Stock = bookModel.Stock;
            book.PhysicalBook.IsAvailable = bookModel.IsAvailable;
            book.PhysicalBook.Width = bookModel.Width;
            book.PhysicalBook.Height = bookModel.Height;
            book.PhysicalBook.Thickness = bookModel.Thickness;
            book.PhysicalBook.Weight = bookModel.Weight;
            book.PhysicalBook.Price = bookModel.Price;
            book.PhysicalBook.FinalPrice = bookModel.FinalPrice;
            // book.PhysicalBook.Discount = new Discount();
            book.PhysicalBook.Isbn = bookModel.Isbn;

            await productService.UpdateBookAsync(book);
            return RedirectToAction("Details", new { book.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteBookAsync(id);
            return RedirectToAction("Index");
        }
    }
}
