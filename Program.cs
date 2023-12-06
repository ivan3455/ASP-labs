using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Додавання контролерів і View)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Дозвіл для статичних файлів (сss)
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // Маршрут для завантаження файлу
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=File}/{action=DownloadFile}/{id?}"
    );
});

app.Run();
