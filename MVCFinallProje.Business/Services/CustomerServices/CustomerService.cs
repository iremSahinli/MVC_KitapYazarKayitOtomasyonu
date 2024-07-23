using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCFinallProje.Business.DTOs.AuthorDTOs;
using MVCFinallProje.Business.DTOs.CustomerDTOs;
using MVCFinallProje.Business.Services.AccountServices;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Domain.Enums;
using MVCFinallProje.Domain.Utilities.Concretes;
using MVCFinallProje.Domain.Utilities.Interfaces;
using MVCFinallProje.Infrastructure.Repositories.CustomerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IAccountService accountService, ICustomerRepository customerRepository)
        {
            _accountService = accountService;
            _customerRepository = customerRepository;
        }



        public async Task<IResult> AddAsync(CustomerCreateDTO customerCreateDTO)
        {
            if (await _accountService.AnyAsync(x => x.Email == customerCreateDTO.Email))
            {
                return new ErrorResult("Emal adresi kullanılıyor");
            }
            IdentityUser user = new()
            {
                Email = customerCreateDTO.Email,
                NormalizedEmail = customerCreateDTO.Email.ToUpperInvariant(),
                UserName = customerCreateDTO.Email,
                NormalizedUserName = customerCreateDTO.Email.ToUpperInvariant(),
                EmailConfirmed = true

            };
            Result result = new ErrorResult();
            var strategy = await _customerRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var identityResult = await _accountService.CreateUserAsync(user, Roles.Customer);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorResult(identityResult.ToString());
                        transactionScope.Rollback();
                        return; //bundan sonraki işlemleri yapmasın diye metotdan çıkartıyorum.
                    }
                    var newCustomer = customerCreateDTO.Adapt<Customer>();
                    newCustomer.IdentityId = user.Id; //Identity ıd si user ıd sine eşit oldu.
                    await _customerRepository.AddAsync(newCustomer);
                    await _customerRepository.SaveChangeAsync();
                    result = new SuccessResult("Müşteri Ekleme işlemi başarılı");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {

                    result = new ErrorResult("Hata: " + ex.Message);
                    transactionScope.Rollback();// Hataya düşerse altta biriken işlemleri iptal et.
                }
                finally
                {
                    transactionScope.Dispose(); //Bu scope de biriken işleri temizler.
                }

            });
            return result;

        }





        public async Task<IResult> DeleteAsync(Guid id)
        {
            Result result = new ErrorResult();
            var strategy = await _customerRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transationScope = await _customerRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var customer = await _customerRepository.GetByIdAsync(id);
                    if (customer == null)
                    {
                        result = new ErrorResult("Müşteri Bulunamadı");
                        transationScope.Rollback(); //Müşteri bulunamazsa transaction geri alınacak.
                        return;
                    }
                    _customerRepository.DeleteAsync(customer);
                    await _customerRepository.SaveChangeAsync();

                    result = new SuccessResult("Müşteri Silme İşlemi Başarılı");
                    transationScope.Commit();  //Veritabanı işlemlerinde işlem bloğuna kaydedilen bütün verileri kalıcı yapar.
                }
                catch (Exception ex)
                {

                    result = new ErrorResult("Hata : " + ex.Message);
                    transationScope.Rollback();
                }
                finally
                {
                    transationScope.Dispose(); //Kaynakları serbest bırak ve verileri temizle.
                }
            });
            return result;
        }

        public async Task<IDataResult<List<CustomerListDTO>>> GetAllAsync()
        {
            var customersDTOs = (await _customerRepository.GetAllAsync()).Adapt<List<CustomerListDTO>>();
            return new SuccessDataResult<List<CustomerListDTO>>(customersDTOs, "Müşteri Listeleme Başarılı");
        }

        public async Task<IDataResult<CustomerDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    return new ErrorDataResult<CustomerDTO>("Güncellenecek Müşteri Bulunamadı");
                }
                return new SuccessDataResult<CustomerDTO>(customer.Adapt<CustomerDTO>(), "Güncellenecek Müşteri Getirildi");
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<CustomerDTO>("Hata : " + ex.Message);
            }
        }

        public async Task<IResult> UpdateAsync(CustomerUpdateDTO customerUpdateDTO)
        {
            Result result = new ErrorResult();
            var strategy = await _customerRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                var transactionScope = await _customerRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var updatingCustomer = await _customerRepository.GetByIdAsync(customerUpdateDTO.Id);
                    if (updatingCustomer == null)
                    {
                        result = new ErrorResult("Güncellenecek Müşteri Bulunamadı");
                        transactionScope.Rollback();
                        return;
                    }
                    var updatedCustomer = customerUpdateDTO.Adapt(updatingCustomer);
                    _customerRepository.UpdateAsync(updatedCustomer);
                    await _customerRepository.SaveChangeAsync();
                    result = new SuccessResult("Müşteri Güncelleme İşlmei Başarılı");
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {

                    result = new ErrorResult("Hata: " + ex.Message);
                }
                finally
                {
                    transactionScope.Dispose();
                }
            });
            return result;
        }
    }
}
