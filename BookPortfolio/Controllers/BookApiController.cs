using BookPortfolio.Data;
using BookPortfolio.Dtos.Book;
using BookPortfolio.Interfaces;
using BookPortfolio.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BookPortfolio.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;
        public BookApiController(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Helpers.BookQueryHelper query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var books = await _bookRepository.GetAllAsync(query);
            var bookDto = books.Select(b => b.ToBookDto()).ToList();
            return Ok(bookDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book.ToBookDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequestDto bookRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookModel = bookRequestDto.ToBookFromCreateDto();
            await _bookRepository.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel.ToBookDto());

        }

    }
}
