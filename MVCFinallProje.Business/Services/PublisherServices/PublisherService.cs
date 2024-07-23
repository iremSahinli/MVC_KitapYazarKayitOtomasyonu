using Mapster;
using MVCFinallProje.Business.DTOs.PublisherDTOs;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Domain.Utilities.Concretes;
using MVCFinallProje.Domain.Utilities.Interfaces;
using MVCFinallProje.Infrastructure.Repositories.PublisherRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.PublisherServices
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }




        public async Task<IResult> AddAsync(PublisherCreateDTO publisherCreateDTO)
        {
            if (await _publisherRepository.AnyAsync(x => x.Name.ToLower() == publisherCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Yayınevi sistemde kayıtlı");
            }
            try
            {
                var newPublisher = publisherCreateDTO.Adapt<Publisher>();
                await _publisherRepository.AddAsync(newPublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccessResult("Yayınevi Ekleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata: " + ex.Message);
            }


        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingPublisher = await _publisherRepository.GetByIdAsync(id);
            if (deletingPublisher == null)
            {
                return new ErrorResult("Silinecek Yayınevi bulunamadı");
            }
            try
            {
                await _publisherRepository.DeleteAsync(deletingPublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccessResult("Yayınevi Silme Başarılı");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Hata: " + ex.Message);
            }
        }



        public async Task<IDataResult<List<PublisherListDTO>>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAllAsync();
            var publisherListDTOs = publishers.Adapt<List<PublisherListDTO>>();
            if (publishers.Count() <= 0)
            {
                return new ErrorDataResult<List<PublisherListDTO>>(publisherListDTOs, "Listelenecek Yayınevi Bulunamadı");

            }
            return new SuccessDataResult<List<PublisherListDTO>>(publisherListDTOs, "Yayınevi Listeleme Başarılı");
        }

        public async Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null)
            {
                return new ErrorDataResult<PublisherDTO>(publisher.Adapt<PublisherDTO>(), "Yayınevi Bulunamadı");
            }
            return new SuccessDataResult<PublisherDTO>(publisher.Adapt<PublisherDTO>(), "Yayınevi Bulundu");

        }

        public async Task<IResult> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO)
        {
            var updatingPublisher = await _publisherRepository.GetByIdAsync(publisherUpdateDTO.Id);
            if (updatingPublisher is null)
            {
                return new ErrorResult("Güncellenecek Yayınevi Bulunamadı");
            }
            try
            {
                var updatedPublisher = publisherUpdateDTO.Adapt(updatingPublisher);
                await _publisherRepository.UpdateAsync(updatedPublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccessResult("Yayınevi Güncelleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult("Hata: " + ex.Message);
            }
        }
    }
}
