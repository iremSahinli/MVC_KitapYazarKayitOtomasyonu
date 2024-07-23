using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinallProje.Business.DTOs.PublisherDTOs;
using MVCFinallProje.Business.Services.PublisherServices;
using MVCFinallProje.UI.Areas.Admin.Models.PublisherVMs;

namespace MVCFinallProje.UI.Areas.Admin.Controllers
{
    public class PublisherController : AdminBaseController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _publisherService.GetAllAsync();
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(result.Data.Adapt<List<AdminPublisherListVM>>());
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<List<AdminPublisherListVM>>());


        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminPublisherCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var publisherCreateDTO = model.Adapt<PublisherCreateDTO>();
            var result = await _publisherService.AddAsync(publisherCreateDTO);
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(model);

            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _publisherService.GetByIdAsync(id);
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<AdminPublisherUpdateVM>());
        }


        [HttpPost]
        public async Task<IActionResult> Update(AdminPublisherUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _publisherService.UpdateAsync(model.Adapt<PublisherUpdateDTO>());
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _publisherService.DeleteAsync(id);

            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }



    }
}
