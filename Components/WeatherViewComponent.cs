using System.Net.Http;
using System.Threading.Tasks;
using AspNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetMVC.Components
{
    // Відображення погоди для заданого регіону
    public class WeatherViewComponent : ViewComponent
    {
        // Отримання погодних даних для заданого регіону
        public async Task<IViewComponentResult> InvokeAsync(string region)
        {
            // Створення HttpClient для взаємодії з API
            using (var httpClient = new HttpClient())
            {
                var apiKey = "c638570cd23c4637aaf32e116eeaceb7";
                var apiUrl =
                    $"https://api.openweathermap.org/data/2.5/weather?q={region}&appid={apiKey}&units=metric";

                // Отримання відповіді з API за допомогою GET-запиту
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Отримання вмісту відповіді у вигляді рядка
                    var content = await response.Content.ReadAsStringAsync();

                    // Парсинг рядка JSON у динамічний об'єкт
                    dynamic jsonObject = JsonConvert.DeserializeObject(content);

                    WeatherModel weather = new WeatherModel
                    {
                        City = jsonObject.name,
                        Temperature = jsonObject.main.temp,
                        Weather = jsonObject.weather[0].main,
                        WeatherDescription = jsonObject.weather[0].description,
                        Humidity = jsonObject.main.humidity,
                        WindSpeed = jsonObject.wind.speed,
                    };

                    return View("/Views/Product/Weather.cshtml", weather);
                }
            }
            return Content("Помилка отримання погоди");
        }
    }
}
