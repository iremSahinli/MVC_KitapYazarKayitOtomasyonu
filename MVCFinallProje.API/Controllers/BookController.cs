using Microsoft.AspNetCore.Mvc;
using MVCFinallProje.Business.DTOs.BookDTOs;
using MVCFinallProje.Business.Services.BookServices;

namespace MVCFinallProje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAllAsync();
            if (result == null)
            {
                return BadRequest();

            }
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDTO bookCreateDTO)
        {


            var createdBook = await _bookService.AddAsync(bookCreateDTO);
            if (createdBook is null)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BookUpdateDTO bookUpdateDTO)
        {
            var update = await _bookService.UpdateAsync(bookUpdateDTO);
            if (update is null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _bookService.DeleteAsync(id);
            if (delete is null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

