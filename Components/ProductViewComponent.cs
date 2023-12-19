using AspNetMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMVC.Components
{
    public class ProductViewComponent : ViewComponent
    {
        // Відображення списку продуктів у представленні
        public IViewComponentResult Invoke(List<ProductModel> products)
        {
            return View("/Views/Product/ProductsTable.cshtml", products);
        }
    }
}
