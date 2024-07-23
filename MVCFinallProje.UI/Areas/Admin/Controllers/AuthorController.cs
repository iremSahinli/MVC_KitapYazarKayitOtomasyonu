using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Business.Services.AuthorServices;
using MVCFinallProje.UI.Areas.Admin.Models.AuthorVMs;

namespace MVCFinallProje.UI.Areas.Admin.Controllers
{

    public class AuthorController : AdminBaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }



        public async Task<IActionResult> Index()
        {
            var result = await _authorService.GetAllAsync();
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(result.Data.Adapt<List<AdminAuthorListVM>>());
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<List<AdminAuthorListVM>>());
        }





        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAuthorCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);  //Önceden girilmeyen bilgiler veya hatalı yerler için modeli tekrar geri döndürüyoruz.
            }
            var authorCreateDTO = model.Adapt<AuthorCreateDTO>();
            var result = await _authorService.AddAsync(authorCreateDTO);

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
            var result = await _authorService.DeleteAsync(id);
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
            var result = await _authorService.GetByIdAsync(id);
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return View(result.Data.Adapt<AdminAuthorUpdateVM>());
        }


        [HttpPost]
        public async Task<IActionResult> Update(AdminAuthorUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var result = await _authorService.UpdateAsync(model.Adapt<AuthorUpdateDTO>());
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
