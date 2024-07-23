using Microsoft.Extensions.DependencyInjection;
using MVCFinallProje.Business.Services.AccountServices;
using MVCFinallProje.Business.Services.AuthorServices;
using MVCFinallProje.Business.Services.BookServices;
using MVCFinallProje.Business.Services.CustomerServices;
using MVCFinallProje.Business.Services.PublisherServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Business.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>(); //Servis ekledik
            services.AddScoped<IPublisherService, PublisherService>();//Servisleri yazdıktan sonra buraya ekledik.
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;

        }
    }
}

//Program.cs kısmına yazmamız gereken servisleri ilgili katmanlarda dependency Injectin şeklinde ayrı classlarda yazıyoruz.