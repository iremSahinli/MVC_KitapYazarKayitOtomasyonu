using Mapster;
using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Domain.Utilities.Concretes;
using MVCFinallProje.Domain.Utilities.Interfaces;
using MVCFinallProje.Infrastructure.Repositories.AuthorRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.AuthorServices
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;  //Dependency Injection

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IResult> AddAsync(AuthorCreateDTO authorCreateDTO)
        {

            if (await _authorRepository.AnyAsync(x => x.Name.ToLower() == authorCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Yazar sistemde kayıtlı");
            }
            try
            {
                var newAuthor = authorCreateDTO.Adapt<Author>();

                //newAuthor.CreatedBy = "System";
                await _authorRepository.AddAsync(newAuthor);
                await _authorRepository.SaveChangeAsync();
                return new SuccessResult("Yazar Ekleme başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }


        public async Task<IDataResult<List<AuthorListDTO>>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorListDTOs = authors.Adapt<List<AuthorListDTO>>();
            if (authors.Count() <= 0)
            {
                return new ErrorDataResult<List<AuthorListDTO>>(authorListDTOs, "Listelenecek Yazar Bulunamadı");

            }
            return new SuccessDataResult<List<AuthorListDTO>>(authorListDTOs, "Yazar listeleme Başarılı");

        }


        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingAuthor = await _authorRepository.GetByIdAsync(id);
            if (deletingAuthor == null)
            {
                return new ErrorResult("Silinecek Yazar Bulunamadı");
            }
            try
            {
                await _authorRepository.DeleteAsync(deletingAuthor);
                await _authorRepository.SaveChangeAsync();
                return new SuccessResult("Yazar Silme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata : " + ex.Message);
                //Silmek için id dışında herhangi bir veriye ihtiyaç olmadığından DTO kullanmadık.
            }

        }

        public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return new ErrorDataResult<AuthorDTO>("Güncellenecek Yazar Bulunamadı");
                }
                return new SuccessDataResult<AuthorDTO>(author.Adapt<AuthorDTO>(), "Güncellenecek Yazar Getirildi");
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<AuthorDTO>("Hata : " + ex.Message);
            }

        }

        public async Task<IResult> UpdateAsync(AuthorUpdateDTO authorUpdateDTO)
        {
            var updatingAuthor = await _authorRepository.GetByIdAsync(authorUpdateDTO.Id);
            if (updatingAuthor is null)
            {
                return new ErrorResult("Güncellenecek Yazar Bulunamadı");
            }
            try
            {
                var updatedAuthor = authorUpdateDTO.Adapt(updatingAuthor);
                await _authorRepository.UpdateAsync(updatedAuthor);
                await _authorRepository.SaveChangeAsync();
                return new SuccessResult("Yazar Güncelleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata : " + ex.Message);
            }
        }
    }
}
