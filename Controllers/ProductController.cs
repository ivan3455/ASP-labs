using AspNetMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductList()
        {
            var products = new List<Product>
            {
                new Product { ID = 1, Name = "Маргарита", Price = 299.99, CreatedDate = DateTime.Now },
                new Product { ID = 2, Name = "Піца шаурма", Price = 310, CreatedDate = DateTime.Now.AddMinutes(10) },
                new Product { ID = 3, Name = "4 м'яса", Price = 329.99, CreatedDate = DateTime.Now.AddMinutes(15) },
                new Product { ID = 4, Name = "Вершкова", Price = 290, CreatedDate = DateTime.Now.AddMinutes(20) },
                new Product { ID = 5, Name = "4 сири", Price = 210, CreatedDate = DateTime.Now.AddMinutes(25) },
                new Product { ID = 6, Name = "Баварська", Price = 360.99, CreatedDate = DateTime.Now.AddDays(30) }
            };
            return View(products);
        }
    }
}
