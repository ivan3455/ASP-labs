using Microsoft.AspNetCore.Mvc.Filters; // Підключення бібліотеки фільтрів ASP.NET Core
using System.Text;
using System.IO;
using System;

namespace AspNetMVC.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly string logFilePath = "log.txt"; // Шлях до файлу журналу логів

        public void OnActionExecuting(ActionExecutingContext context) // Метод, що викликається перед виконанням дії контролера
        {
            string actionName = context.ActionDescriptor.DisplayName; // Отримання імені дії, яка викликається
            string timestamp = DateTime.Now.ToString(); // Отримання поточного часу

            string logEntry = $"Action '{actionName}' accessed at {timestamp}\n"; // Створення запису логу

            File.AppendAllText(logFilePath, logEntry, Encoding.UTF8); // Додавання запису до файлу логів
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Метод, що викликається після виконання дії контролера
        }
    }
}