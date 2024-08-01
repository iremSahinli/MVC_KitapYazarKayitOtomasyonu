using Microsoft.AspNetCore.Identity;
using MVCFinallProje.Infrastructure.AppContext;

namespace MVCFinallProje.API.Extensions
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }

    }
}
