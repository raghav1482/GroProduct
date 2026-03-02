using GroProduct.Data;
using GroProduct.Dtos;
using GroProduct.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroProduct.Controllers
{
    public class AddOrderDto
    {
        public int UserId { get; set; }
        public string Status { get; set; }
        // use decimal to match Order.TotalAmount and avoid lossy conversions
        public decimal TotalAmount { get; set; } = 0;

        // make sure we always have a collection to avoid null refs when binding
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private AppDbContext db_context;
        public OrdersController(AppDbContext context)
        {
            db_context = context;
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            // include related collections so client gets full order info
            var orders = await db_context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return Ok(orders);
        }



        [HttpPost("addOrder")]
        public async Task<IActionResult> AddOrder(AddOrderDto model)
        {
            // generate the order first so we can wire up FKs explicitly if necessary
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = model.UserId,
                Status = model.Status,
                TotalAmount = model.TotalAmount
            };

            if (model.OrderItems != null && model.OrderItems.Any())
            {
                // assign OrderId explicitly and map items
                order.OrderItems = model.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    OrderId = order.Id
                }).ToList();
            }

            await db_context.Orders.AddAsync(order);
            await db_context.SaveChangesAsync();

            // return the order including related entities so the client sees the items
            var createdOrder = await db_context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstAsync(o => o.Id == order.Id);

            return Ok(createdOrder);
        }

        [HttpDelete("deleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await db_context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            db_context.Orders.Remove(order);
            await db_context.SaveChangesAsync();
            return Ok(order);
        }


        [HttpPut("updateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, AddOrderDto order)
        {
            var existingOrder = await db_context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.UserId = order.UserId;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            await db_context.SaveChangesAsync();
            return Ok(existingOrder);

        }

    }
}
