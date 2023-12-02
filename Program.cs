var builder = WebApplication.CreateBuilder(args);

// Додання кешу в пам'яті до сервісів додатка
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();

// Отримання поточної дати та часу для використання у логах помилок
DateTime currentTime = DateTime.Now;

// Визначення шляху до файлу журналу помилок
var ErrorLogsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ErrorLog/ErrorLogs.txt");
app.UseDeveloperExceptionPage();

app.Map("/", (context) =>
{
    // Отримання шляху до HTML-файлу форми
    var htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "html/form.html");

    if (File.Exists(htmlFilePath))
    {
        var htmlContent = File.ReadAllText(htmlFilePath);

        return context.Response.WriteAsync(htmlContent);
    }
    else
    {
        context.Response.StatusCode = 404;
        return Task.CompletedTask;
    }
});

app.Map("/Submit", (context) =>
{
    var htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "html/submit.html");

    if (File.Exists(htmlFilePath))
    {
        var htmlContent = File.ReadAllText(htmlFilePath);

        if (context.Request.Method == "POST")
        {
            var inputValue = context.Request.Form["inputValue"];
            var inputDateAndTime = DateTime.Parse(context.Request.Form["inputDateAndTime"]);

            var cookieOptions = new CookieOptions
            {
                Expires = inputDateAndTime,
                HttpOnly = false
            };

            context.Response.Cookies.Append("formData", inputValue, cookieOptions);

            htmlContent = htmlContent.Replace("{{Message}}", "Дані були успішно збережені в Cookie.");
        }
        else
        {
            htmlContent = htmlContent.Replace("{{Message}}", "Неправильний метод запиту.");
        }

        context.Response.ContentType = "text/html; charset=utf-8";
        return context.Response.WriteAsync(htmlContent);
    }
    else
    {
        context.Response.StatusCode = 404;
        return Task.CompletedTask;
    }
});

app.Map("/CheckCookieValue", (context) =>
{
    var htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "html/checkCookieValue.html");

    if (File.Exists(htmlFilePath))
    {
        var htmlContent = File.ReadAllText(htmlFilePath);

        if (context.Request.Cookies.TryGetValue("formData", out var formData))
        {
            htmlContent = htmlContent.Replace("{{CookieValue}}", $"В Cookie були знайдені наступні значення: {formData}");
        }
        else
        {
            htmlContent = htmlContent.Replace("{{CookieValue}}", "В Cookie не знайдено жодних значень.");
        }

        context.Response.ContentType = "text/html; charset=utf-8";
        return context.Response.WriteAsync(htmlContent);
    }
    else
    {
        context.Response.StatusCode = 404;
        return Task.CompletedTask;
    }
});


app.Map("/GenerateError", (context) =>
{
    throw new Exception("Це тестова помилка!");
});

// Налаштування middleware, який записує інформацію про помилку до файлу логів
app.UseExceptionHandler(app => app.Run(async context =>
{
    File.WriteAllText(ErrorLogsFilePath, "Помилка під час виконання запиту " + currentTime);
    context.Response.StatusCode = 500;
    await context.Response.WriteAsync("Error 500.");
}));

app.Run();