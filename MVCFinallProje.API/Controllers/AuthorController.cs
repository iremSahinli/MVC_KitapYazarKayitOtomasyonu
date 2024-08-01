using Microsoft.AspNetCore.Mvc;
using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Business.Services.AuthorServices;

namespace MVCFinallProje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _authorService.GetAllAsync();
            if (result == null)
            {
                return BadRequest();

            }
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateDTO authorCreateDTO)
        {


            var createdAuthor = await _authorService.AddAsync(authorCreateDTO);
            if (createdAuthor is null)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AuthorUpdateDTO authorUpdateDTO)
        {
            var update = await _authorService.UpdateAsync(authorUpdateDTO);
            if (update is null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _authorService.DeleteAsync(id);
            if (delete is null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
