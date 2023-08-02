using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.BookServices;

namespace APILibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: /books/titles
        [HttpGet("titles")]
        public async Task<IActionResult> GetAllTitles([FromQuery] PagingParameters p, [FromQuery] FilterSettings? f, [FromQuery]  string? title, [FromQuery] string? name)
        {
            var titles = await _bookService.GetAllTitles(p,f,title,name);
            return Ok(titles);
        }

        

        // GET: /books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookDetailsById(int id)
        {
            var book = await _bookService.GetBookDetailsById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // GET: /books?title={title}
        [HttpGet]
        public async Task<IActionResult> GetBookDetailsByName([FromQuery] string title)
        {
            var book = await _bookService.GetBookDetailsByName(title);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // PUT: /books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBookDetails(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest();

            var editedBook = await _bookService.EditBookDetails(book);
            return Ok(editedBook);
        }

        // DELETE: /books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }

        // POST: /books
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var newBook = await _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBookDetailsById), new { id = newBook.Id }, newBook);
        }

       
    }
}
