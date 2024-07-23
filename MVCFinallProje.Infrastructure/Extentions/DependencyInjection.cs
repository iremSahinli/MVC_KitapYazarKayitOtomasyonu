using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCFinallProje.Infrastructure.AppContext;
using MVCFinallProje.Infrastructure.Repositories.AuthorRepositories;
using MVCFinallProje.Infrastructure.Repositories.BookRepositories;
using MVCFinallProje.Infrastructure.Repositories.CustomerRepository;
using MVCFinallProje.Infrastructure.Repositories.PublisherRepository;
using MVCFinallProje.Infrastructure.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));
            });

            services.AddScoped<IAuthorRepository, AuthorRepository>();  //Bunuda buraya ekliyoruz.
            services.AddScoped<IPublisherRepository, PublisherRepository>(); //Publisher servisleri için ekledik.
            services.AddScoped<IBookRepository, BookRepository>(); //BookServislerini ekledik.
            services.AddScoped<ICustomerRepository, CustomerRepository>();


            //Seed Data (Genelde Mig işlemlerinde yoruma almak zorunda kalabiliriz.)
            AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();


            return services;
        }
    }
}

//AppSettings Json u Iconfiguration temsil eder

