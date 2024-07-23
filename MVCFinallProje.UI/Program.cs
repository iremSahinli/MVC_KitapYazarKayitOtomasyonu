using MVCFinallProje.Infrastructure.Extentions;
using MVCFinallProje.Business.Extentions;
using MVCFinallProje.UI.Extentions;
using Microsoft.EntityFrameworkCore;
using MVCFinallProje.Infrastructure.AppContext;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddUIServices();







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //sisteme login olmasý ve ilgili role gitmesi.
app.UseAuthorization();  //Yetkilendirme

app.MapControllerRoute(
    name: "areas",
            pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");
app.MapDefaultControllerRoute(); //Areasýz routelerimiz için ekliyoruz.

app.Run();
