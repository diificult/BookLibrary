using BookPortfolio.Data;
using BookPortfolio.Dtos.Book;
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
        private readonly UserManager<ApplicationDbContext> _userManager;
        private readonly BookRepository _bookRepository;

        public BookController(ApplicationDbContext context, UserManager<ApplicationDbContext> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public async Task<IActionResult> GetAll([FromQuery] Helpers.BookQueryHelper query)
        {
            if (!ModelState.IsValid)
            {
                return View(query);
            }
            var books = await _bookRepository.GetAllAsync(query);
            var bookDto = books.Select(b => b.ToBookDto()).ToList();
            return View(bookDto);
        }
    }
}
