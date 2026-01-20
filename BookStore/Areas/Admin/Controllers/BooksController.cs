using BookStore.Services;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
                NumberInSeries = 1,
                DigitalBook = new DigitalBook
                {
                    Slug = bookModel.Slug,
                    FileName = bookModel.File == null ? "" : bookModel.File.FileName,
                    IsAvailable = bookModel.EIsAvailable,
                    Price = bookModel.EPrice,
                    FinalPrice = bookModel.EFinalPrice,
                    //Discount = new Discount(),
                    ISBN = bookModel.EIsbn
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
                    ISBN = bookModel.Isbn
                }
            };

            await productService.AddBookAsync(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
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
