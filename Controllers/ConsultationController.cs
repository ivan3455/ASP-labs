using AspNetMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class ConsultationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public string Consultation(ConsultationModel model)
    {
        if (ModelState.IsValid) // Перевірка на валідність введених даних
        {
            return $"Дані пройшли валідацію: {model.Name} - {model.Email} - {model.ConsultationDate} - {model.SelectedProduct}";
            // Повертає рядок із валідними даними консультації
        }
        return "Дані не пройшли валідацію"; // Повертає повідомлення про невалідність даних
    }
}
