using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AuthorServices;
using Services;

namespace APILibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetAllAuthors([FromQuery] PagingParameters p)
        {
            var authors = await _authorService.GetAllAuthors(p);
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorDetailsById(int id)
        {
            var author = await _authorService.GetAuthorDetailsById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Author>> GetAuthorDetailsByNameAndLastname(
            [FromQuery] string name, [FromQuery] string lastname)
        {
            var author = await _authorService.GetAuthorDetailsByNameAndLastname(name, lastname);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> EditAuthorDetails(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            try
            {
                var editedAuthor = await _authorService.EditAuthorDetails(author);
                return Ok(editedAuthor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<int>> DeleteAuthor(int id)
        //{
        //    try
        //    {
        //        var deletedAuthorId = await _authorService.DeleteAuthor(id);
        //        return Ok(deletedAuthorId);
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An error occurred while processing the request.");
        //    }
        //}
    }
}