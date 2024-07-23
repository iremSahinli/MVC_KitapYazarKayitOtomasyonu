using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFinallProje.Business.DTOs.BookDTOs;
using MVCFinallProje.Business.Services.AuthorServices;
using MVCFinallProje.Business.Services.BookServices;
using MVCFinallProje.Business.Services.PublisherServices;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Domain.Utilities.Concretes;
using MVCFinallProje.UI.Areas.Admin.Models.AuthorVMs;
using MVCFinallProje.UI.Areas.Admin.Models.BookVMs;

namespace MVCFinallProje.UI.Areas.Admin.Controllers
{
    public class BookController : AdminBaseController
    {

        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllAsync();
            var adminBookListVM = result.Data.Adapt<List<AdminBookListVM>>();
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(adminBookListVM);
            }
            SuccessNotfy(result.Message);
            return View(adminBookListVM);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            AdminBookCreateVM vm = new AdminBookCreateVM();
            vm.Authors = await GetAuthors();
            vm.Publishers = await GetPublishers();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminBookCreateVM model)
        {
            var createBookDTO = model.Adapt<BookCreateDTO>();
            var result = await _bookService.AddAsync(createBookDTO);
            if (!result.IsSucces)
            {
                ErrorNotyf(result.Message);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");
        }

        private async Task<SelectList> GetPublishers(Guid? publisherId = null) //Metotlarımızı yazdık ve Action metotlarımıza ekledik.
        {
            var publishers = (await _publisherService.GetAllAsync()).Data;

            return new SelectList(publishers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (publisherId != null ? publisherId.Value : publisherId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }

        private async Task<SelectList> GetAuthors(Guid? authorId = null)
        {
            var authors = (await _authorService.GetAllAsync()).Data;

            return new SelectList(authors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (authorId != null ? authorId.Value : authorId)

            }).OrderBy(x => x.Text), "Value", "Text");
        }  //Metotlarımızı yazdık ve Action metotlarımıza ekledik.


        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _bookService.GetByIdAsync(id);
            if (result == null)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            var vm = result.Data.Adapt<AdminBookUpdateVM>();  
            vm.Authors = await GetAuthors(result.Data.AuthorId);
            vm.Publishers = await GetPublishers(result.Data.PublisherId);

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminBookUpdateVM model)
        {
            var updatedBookDTO = model.Adapt<BookUpdateDTO>();
            var result = await _bookService.UpdateAsync(updatedBookDTO);
            if (result == null)
            {
                ErrorNotyf(result.Message);
                model.Authors = await GetAuthors(model.AuthorId);
                model.Publishers = await GetPublishers(model.PublisherId);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Index");

        }







    }
}
