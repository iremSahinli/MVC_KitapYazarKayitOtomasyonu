using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Business.DTOs.BookDTOs;
using MVCFinallProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.BookServices
{
    public interface IBookService
    {
        Task<IResult> AddAsync(BookCreateDTO bookCreateDTO);
        Task<IDataResult<List<BookListDTO>>> GetAllAsync();
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<BookDTO>> GetByIdAsync(Guid id);
        Task<IResult> UpdateAsync(BookUpdateDTO bookUpdateDTO);
    }
}
