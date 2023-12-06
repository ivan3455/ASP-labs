using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Models;

public class ProductController : Controller
{
    // Форма реєстрації користувача
    public IActionResult RegisterUser()
    {
        return View();
    }

    // Обробник POST-запиту - реєстрація користувача
    [HttpPost]
    public IActionResult RegisterUser(User user)
    {
        if (user.Age >= 16)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Гавайська",
                    Quantity = 0,
                    Price = 200
                },
                new Product
                {
                    Name = "Піца шаурма",
                    Quantity = 0,
                    Price = 300
                },
                new Product
                {
                    Name = "Баварська",
                    Quantity = 0,
                    Price = 250
                },
                new Product
                {
                    Name = "Вершкова",
                    Quantity = 0,
                    Price = 240
                },
                new Product
                {
                    Name = "4 сири",
                    Quantity = 0,
                    Price = 270
                },
                new Product
                {
                    Name = "Маргарита",
                    Quantity = 0,
                    Price = 240
                },
                new Product
                {
                    Name = "4 м'яса",
                    Quantity = 0,
                    Price = 310
                }
            };
            user.Products = products;
            return View("OrderPizza", user); // Перехід до замовлення піци
        }
        else
        {
            return Content("Ви повинні бути старше 15 років щоб здійснювати замовлення.");
        }
    }

    // Обробник POST-запиту - створення замовлення піци
    [HttpPost]
    public IActionResult OrderPizza(User user)
    {
        return View("OrderSummary", user);
    }

    [HttpPost]
    public IActionResult OrderSummary(List<Product> products)
    {
        return View(products);
    }
}
