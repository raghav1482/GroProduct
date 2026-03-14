using Microsoft.AspNetCore.Mvc;

namespace GroProduct.Controllers
{
    [Route("/view-products/[controller]")]
    public class ProductsViewController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            // Use the conventional view lookup: /Views/ProductsView/Index.cshtml
            return View();
        }
    }
}
