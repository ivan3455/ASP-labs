var builder = WebApplication.CreateBuilder();

// Додання служб для ін'єкції залежностей: Book, User, UserService
builder.Services.AddTransient<Book>();
builder.Services.AddTransient<User>();
builder.Services.AddSingleton<UserService>();

// Додання конфігурації з файлу JSON для книг
builder.Configuration.AddJsonFile("Config/Books/books.json");

var app = builder.Build();
var configuration = app.Services.GetService<IConfiguration>();

// Отримання шляху до файла з користувачами
var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config/Users");

if (File.Exists(jsonFilePath))
{
    var jsonContent = File.ReadAllText(jsonFilePath);
}

app.Map(
    "/",
    async (context) =>
    {
        // Встановлення типу контенту для відповіді
        context.Response.ContentType = "text/html; charset=utf-8"; // Встановлюємо тип контенту як HTML з кодуванням UTF-8

        // Відправлення HTML з посиланням на сторінку бібліотеки
        await context.Response.WriteAsync("<a href='/Library/'>Перейти до бібліотеки</a><br>");
    }
);

app.Map(
    "/Library",
    async (context) =>
    {
        context.Response.ContentType = "text/html; charset=utf-8"; // Встановлюємо тип контенту як HTML з кодуванням UTF-8

        await context.Response.WriteAsync("<p>Вітаємо в нашій бібліотеці! <br>");
        await context.Response.WriteAsync("<a href='/Library/Books'>Перейти до книг</a><br>");
        await context.Response.WriteAsync("<a href='/Library/Profile'>Перейти до профілю</a></p>");

        if (!context.Request.Cookies.ContainsKey("loggedIn"))
        {
            await context.Response.WriteAsync("<a href='/Login'>Увійти до системи</a></p>");
        }
        else
        {
            await context.Response.WriteAsync("<a href='/Logout'>Вийти з системи</a></p>");
        }
    }
);


app.Map(
    "/Library/books",
    async (context) =>
    {
        var books = configuration.GetSection("Books").Get<List<Book>>();

        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("<table border='1'>");
        await context.Response.WriteAsync("<tr><th>Назва</th><th>Автор</th><th>Рік</th></tr>");

        foreach (var book in books)
        {
            await context.Response.WriteAsync($"<tr><td>{book.Title}</td><td>{book.Author}</td><td>{book.Year}</td></tr>");
        }

        await context.Response.WriteAsync("</table><br>");
        await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
    }
);

app.MapGet(
    "/Library/Profile/{id:int?}",
    async (HttpContext context, int? id, UserService userService) =>
    {
        if (!id.HasValue)
        {
            // Якщо не вказано ID, перевіряємо авторизацію
            if (!context.Request.Cookies.ContainsKey("loggedIn"))
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("Жоден користувач не увійшов в систему.<br>");
                await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
                return;
            }

            var loggedInUserId = int.Parse(context.Request.Cookies["loggedIn"]);
            var user = userService.GetUserById(loggedInUserId);

            if (user != null)
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Ім'я: {user.UserName}, Вік: {user.Age}, Олюблений жанр: {user.FavoriteGenre}<br>");
                await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
                return;
            }
        }
        else
        {
            var user = userService.GetUserById(id.Value);

            if (user != null)
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Ім'я: {user.UserName}, Вік: {user.Age}, Олюблений жанр: {user.FavoriteGenre}<br>");
                await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
                return;
            }
        }

        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("Дані про користувача не знайдені.<br>");
        await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
    }
);

app.MapGet(
    "/Login/",
    async (context) => {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("Введіть ваші дані в параметри сегментів маршруту - /Login/id/password");
    }
);

app.MapGet(
    "/Login/{id:int}/{pwd}",
    async (HttpContext context, int id, string pwd, UserService userService) =>
    {

        if (userService.AuthenticateUser(id, pwd))
        {
            var user = userService.GetUserById(id);
            context.Response.Cookies.Append("loggedIn",  id.ToString());

            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"Успішний вхід для користувача {user.UserName}!<br>");
            await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
        }
        else
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("Невірне ім'я користувача або пароль.<br>");
            await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
        }
    }
);

app.MapGet(
    "/Logout",
    async (HttpContext context) =>
    {
        context.Response.Cookies.Delete("loggedIn");
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("Ви вийшли з системи.<br>");
        await context.Response.WriteAsync("<a href='/Library/'>На головну</a></p>");
    }
);


app.Run();
