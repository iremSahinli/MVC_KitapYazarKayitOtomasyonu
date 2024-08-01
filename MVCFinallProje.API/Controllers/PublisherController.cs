using Microsoft.AspNetCore.Mvc;
using MVCFinallProje.Business.DTOs.PublisherDTOs;
using MVCFinallProje.Business.Services.PublisherServices;

namespace MVCFinallProje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Publishercontroller : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public Publishercontroller(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _publisherService.GetAllAsync();
            if (result == null)
            {
                return BadRequest();

            }
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create(PublisherCreateDTO publisherCreateDTO)
        {


            var createdAuthor = await _publisherService.AddAsync(publisherCreateDTO);
            if (createdAuthor is null)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Update(PublisherUpdateDTO publisherUpdateDTO)
        {
            var update = await _publisherService.UpdateAsync(publisherUpdateDTO);
            if (update is null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _publisherService.DeleteAsync(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
