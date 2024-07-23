using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Domain.Enums;
using MVCFinallProje.Infrastructure.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MVCFinallProje.Infrastructure.Seeds
{
    public static class AdminSeed
    {
        private const string adminEmail = "admin@bilgeadam.com";
        private const string adminPassword = "Password.1";
        public static async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();  //appsettingsDevelopment.jsonda ulaşmak için.
            dbContextBuilder.UseSqlServer(configuration.GetConnectionString("AppConnectionString")); //parametre olarak options verdik.
            AppDbContext context = new AppDbContext(dbContextBuilder.Options); 
            if (!context.Roles.Any(x => x.Name == "Admin")) //Herhangi bir role varmı yoksa ekle
            {
                await AddRoles(context);

            }
            if (!context.Users.Any(user => user.Email == adminEmail))
            {
                await AddAdmin(context);
            }
        }

        private static async Task AddAdmin(AppDbContext context)
        {
            IdentityUser user = new IdentityUser()
            {
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpperInvariant(),
                UserName = adminEmail,
                NormalizedUserName = adminEmail.ToUpperInvariant(),
                EmailConfirmed = true
            };
            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, adminPassword); //şifre kontrol işlemi.
            await context.Users.AddAsync(user); //users tablosuna bilgileri ekledim.


            var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.Admin.ToString()).Id;  //AspNetUsers tabosunu temsil eder.
            await context.UserRoles.AddAsync(new IdentityUserRole<string>()
            {
                RoleId = adminRoleId,
                UserId = user.Id
            });

            await context.Admins.AddAsync(new Admin()
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = adminEmail,
                IdentityId = user.Id

            });
            await context.SaveChangesAsync();





        }

        private static async Task AddRoles(AppDbContext context)
        {
            string[] roles = Enum.GetNames(typeof(Roles));  //Enumda bulunan verilerin isimlerini dizi olarak döner.

            for (int i = 0; i < roles.Length; i++)
            {
                if (await context.Roles.AnyAsync(x => x.Name == roles[i])) //Admin diye bir rol varmı
                {
                    continue; //döngüye devam et.
                }
                IdentityRole role = new IdentityRole()
                {
                    Name = roles[i],
                    NormalizedName = roles[i]
                };

                await context.Roles.AddAsync(role);
                await context.SaveChangesAsync();

            }
        }
    }
}
