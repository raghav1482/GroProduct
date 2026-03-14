using Microsoft.AspNetCore.Mvc;

namespace GroProduct.Controllers
{
    [Route("view-orders/[controller]")]
    public class OrdersViewController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}