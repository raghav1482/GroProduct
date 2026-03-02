using GroProduct.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroProduct.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private AppDbContext db_context;
        public ProductsController(AppDbContext context)
        {
            db_context = context;
        }



        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await db_context.Products.ToListAsync();
            return Ok(products);
        }


        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct(Models.Product product)
        {
            var tempProduct = await db_context.Products.AddAsync(product);
            await db_context.SaveChangesAsync();

            return Ok("Product added :)");
        }


        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await db_context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("No such product exists");
            }
            db_context.Products.Remove(product);
            await db_context.SaveChangesAsync();
            return Ok("Product deleted :)");


        }

        [HttpPut("updateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Models.Product product)
        {
            var tempProduct = await db_context.Products.FindAsync(id);
            if (tempProduct == null)
            {
                return NotFound("Product not found");
            }
            else
            {
                tempProduct.Name = product.Name;
                tempProduct.Description = product.Description;
                tempProduct.Price = product.Price;
                tempProduct.Balance = product.Balance;
                tempProduct.Amount = product.Amount;
                await db_context.SaveChangesAsync();
                return Ok("Product updated :)");
            }
        }
    }
}
