using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Business.DTOs.CustomerDTOs;
using MVCFinallProje.Business.Services.CustomerServices;
using MVCFinallProje.UI.Areas.Admin.Models.AuthorVMs;
using MVCFinallProje.UI.Areas.Admin.Models.CustomerVMs;

namespace MVCFinallProje.UI.Areas.Admin.Controllers
{
    public class CustomerController : AdminBaseController
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _customerService.GetAllAsync();

            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(result.Data.Adapt<List<AdminCustomerListVM>>());
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<List<AdminCustomerListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AdminCustomerCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var customerCreateDTO = model.Adapt<CustomerCreateDTO>();
            var result = await _customerService.AddAsync(customerCreateDTO);

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
            var result = await _customerService.DeleteAsync(id);
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _customerService.GetByIdAsync(id);
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<AdminCustomerUpdateVM>());
        }



        [HttpPost]
        public async Task<IActionResult> Update(CustomerUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var result = await _customerService.UpdateAsync(model.Adapt<CustomerUpdateDTO>());
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(model);

            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");

        }

    }
}
