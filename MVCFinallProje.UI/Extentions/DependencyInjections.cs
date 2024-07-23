using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using MVCFinallProje.Infrastructure.AppContext;

namespace MVCFinallProje.UI.Extentions
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            services.AddNotyf(config =>  //İşlemlerde sağ alt kısımda işlem durumları için bildirim verir.UI servise bağladık.
            {
                config.HasRippleEffect = true;
                config.DurationInSeconds = 3;
                config.Position = NotyfPosition.BottomRight;
                config.IsDismissable = true;

            });


            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
