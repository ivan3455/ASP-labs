using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Додавання сервісу контролерів і Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Дозвіл на використання статичних файлів (css)
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // Маршрут за замовчуванням для контролера Product та дії RegisterUser
    // Перехід здійснюється при старті додатку
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Product}/{action=RegisterUser}/{id?}");
});

app.Run();
