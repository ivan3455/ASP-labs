using Microsoft.AspNetCore.Mvc.Filters; // Підключення бібліотеки фільтрів ASP.NET Core
using System.Security.Claims; // Підключення бібліотеки для роботи з претензіями (Claims)
using System.Text;
using System.IO;

namespace AspNetMVC.Filters
{
    public class UniqueUsersFilter : IActionFilter
    {
        private readonly string usersFilePath = "users.txt"; // Шлях до файлу, де зберігаються унікальні користувачі

        public void OnActionExecuting(ActionExecutingContext context) // Метод, що викликається перед виконанням дії контролера
        {
            string userIpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString(); // Отримання IP-адреси користувача

            if (!File.ReadAllLines(usersFilePath, Encoding.UTF8).Contains(userIpAddress)) // Перевірка, чи IP-адреса вже існує у файлі
            {
                File.AppendAllText(usersFilePath, userIpAddress + "\n", Encoding.UTF8); // Додавання IP-адреси до файлу, якщо вона ще не існує
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Метод, що викликається після виконання дії контролера
        }
    }
}