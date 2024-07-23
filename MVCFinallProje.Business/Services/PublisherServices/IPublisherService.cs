using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Business.DTOs.PublisherDTOs;
using MVCFinallProje.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.PublisherServices
{
    public interface IPublisherService
    {
        Task<IResult> AddAsync(PublisherCreateDTO publisherCreateDTO);
        Task<IDataResult<List<PublisherListDTO>>> GetAllAsync();
        /// <summary>
        /// Bu metot aldığı parametre sonucu yayınevini siler
        /// </summary>
        /// <param name="id">yayınevi Id</param>
        /// <returns>Başarılı/Başarısız</returns>
        Task<IResult> DeleteAsync(Guid id);

        Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id);
        Task<IResult> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO);
    }
}
