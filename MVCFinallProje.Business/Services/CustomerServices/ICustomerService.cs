using MVCFinallProje.Business.DTOs.BookDTOs;
using MVCFinallProje.Business.DTOs.CustomerDTOs;
using MVCFinallProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<IResult> AddAsync(CustomerCreateDTO customerCreateDTO);
        Task<IDataResult<List<CustomerListDTO>>> GetAllAsync();
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<CustomerDTO>> GetByIdAsync(Guid id);
        Task<IResult> UpdateAsync(CustomerUpdateDTO customerUpdateDTO);
    }
}
