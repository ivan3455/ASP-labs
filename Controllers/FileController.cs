using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CreateFile.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult DownloadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFile(
            string firstName,
            string lastName,
            string fileName
        )
        {
            // Перевірка, чи файл має розширення ".txt", якщо ні - додаємо його
            if (!fileName.EndsWith(".txt"))
            {
                fileName += ".txt";
            }

            // Формування шляху до файлу в папці "downloads" у веб-корені додатка
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "downloads", fileName);

            // Створення вмісту файлу із введеними даними про ім'я та прізвище
            var fileContent = $"Ім'я: {firstName}{Environment.NewLine}Прізвище: {lastName}";

            // Запис вмісту у файл з кодуванням UTF-8
            await System.IO.File.WriteAllTextAsync(filePath, fileContent, Encoding.UTF8);

            // Повернення файлу користувачеві для завантаження
            return PhysicalFile(filePath, "text/plain", fileName);
        }
    }
}
