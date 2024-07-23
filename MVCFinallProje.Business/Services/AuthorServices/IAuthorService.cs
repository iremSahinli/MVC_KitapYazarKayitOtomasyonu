using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.AuthorServices
{
    public interface IAuthorService
    {
        Task<IResult> AddAsync(AuthorCreateDTO authorCreateDTO);
        Task<IDataResult<List<AuthorListDTO>>> GetAllAsync();
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id);
        Task<IResult> UpdateAsync(AuthorUpdateDTO authorUpdateDTO);
    }
}
