using Mapster;
using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Business.DTOs.BookDTOs;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Domain.Utilities.Concretes;
using MVCFinallProje.Domain.Utilities.Interfaces;
using MVCFinallProje.Infrastructure.Repositories.BookRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.BookServices
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }



        public async Task<IResult> AddAsync(BookCreateDTO bookCreateDTO)
        {
            if (await _bookRepository.AnyAsync(x => x.Name.ToLower() == bookCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Kitap sistemde kayıtlı");
            }
            try
            {
                var newBook = bookCreateDTO.Adapt<Book>();

                await _bookRepository.AddAsync(newBook);
                await _bookRepository.SaveChangeAsync();
                return new SuccessResult("Kitap Ekleme başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }
        public async Task<IDataResult<List<BookListDTO>>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookListDTos = books.Adapt<List<BookListDTO>>();
            if (books.Count() <= 0)
            {
                return new ErrorDataResult<List<BookListDTO>>(bookListDTos, "Listelenecek Kitap Bulunamadı");

            }
            return new SuccessDataResult<List<BookListDTO>>(bookListDTos, "Kitap listeleme Başarılı");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingBook = await _bookRepository.GetByIdAsync(id);
            if (deletingBook == null)
            {
                return new ErrorResult("Silinecek Kitap Bulunamadı");
            }
            try
            {
                await _bookRepository.DeleteAsync(deletingBook);
                await _bookRepository.SaveChangeAsync();
                return new SuccessResult("Kitap Silme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata : " + ex.Message);
                //Silmek için id dışında herhangi bir veriye ihtiyaç olmadığından DTO kullanmadık.
            }
        }


        public async Task<IDataResult<BookDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book is null)
                {
                    return new ErrorDataResult<BookDTO>(book.Adapt<BookDTO>(), "Gösterilecek Kitap Bulunamadı");
                }
                return new SuccessDataResult<BookDTO>(book.Adapt<BookDTO>(), "Kitap Getirme Getirildi");
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<BookDTO>("Hata : " + ex.Message);
            }
        }

        public async Task<IResult> UpdateAsync(BookUpdateDTO bookUpdateDTO)
        {
            var updatingBook = await _bookRepository.GetByIdAsync(bookUpdateDTO.Id);
            if (updatingBook is null)
            {
                return new ErrorResult("Güncellenecek Kitap Bulunamadı");
            }
            try
            {
                var updatedAuthor = bookUpdateDTO.Adapt(updatingBook);
                await _bookRepository.UpdateAsync(updatedAuthor);
                await _bookRepository.SaveChangeAsync();
                return new SuccessResult("Kitap Güncelleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata : " + ex.Message);
            }
        }
    }
}
