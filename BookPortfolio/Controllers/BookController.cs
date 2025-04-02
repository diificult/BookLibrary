using BookPortfolio.Data;
using BookPortfolio.Dtos.Book;
using BookPortfolio.Interfaces;
using BookPortfolio.Mappers;
using BookPortfolio.Models;
using BookPortfolio.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPortfolio.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;

        public BookController(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var books = _context.books.ToList();
            return View(books);
        }

        // Post

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var bookModel = model.ToBookFromCreateDto();
            await _bookRepository.CreateAsync(bookModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SearchResults([FromQuery] Helpers.BookQueryHelper query)
        {
            if (!ModelState.IsValid)
            {
                return View(query);
            }
            var books = await _bookRepository.GetAllAsync(query);
            var bookDto = books.Select(b => b.ToBookDto()).ToList();
            Console.WriteLine("Found " + books.Count + " books with the query " + query.SearchTerm);
            return View(books);
        }
    }
}
