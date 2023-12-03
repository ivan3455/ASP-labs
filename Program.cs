var builder = WebApplication.CreateBuilder();
var app = builder.Build();

builder.Configuration.AddJsonFile("Config/User.json"); // Додавання конфігурації з файлу JSON

var configuration = app.Configuration;
var service = new CompanyService(); // Створення сервісу для роботи з компаніями

var apple = service.ReadJsonFile("Config/Apple.json");
var google = service.ReadXmlFile("Config/Google.xml");
var microsoft = service.ReadIniFile("Config/Microsoft.ini");

var companyWithMostEmployees = service.CompanyWithMostEmployees(apple, google, microsoft);

app.MapGet(
    "/",
    async (context) =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context
            .Response
            .WriteAsync("<a href='/user'>Перейти до інформації про користувача</a><br>");
        await context
            .Response
            .WriteAsync("<a href='/companies'>Перейти до інформації про комапнії</a><br>");
    }
);

app.MapGet(
    "/user",
    async (context) =>
    {
        var user = configuration.Get<User>();

        context.Response.ContentType = "text/html; charset=utf-8";
        await context
            .Response
            .WriteAsync(
                $"{user.Name} {user.Surname}<br>Вік: {user.Age}<br> Країна: {user.Country_of_birth}"
            );
    }
);

app.MapGet(
    "/companies",
    async (context) =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        var companyList = new List<Company> { apple, google, microsoft }; // Список всіх компаній
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("<h1>Інформація про компанії</h1>");

        foreach (var company in companyList)
        {
            await context
                .Response
                .WriteAsync(
                    $"Назва: {company.Company_name}<br>"
                        + $"Кількість співробітників: {company.Number_of_employees}<br>"
                        + $"Країна: {company.Country_of_origin}<br><br>"
                );
        }
        await context
            .Response
            .WriteAsync(
                $"Найбільше співробітників у компанії - {companyWithMostEmployees.Company_name}<br>"
                    + $"Кількість співробітників: {companyWithMostEmployees.Number_of_employees}"
            );
    }
);

app.Run();
