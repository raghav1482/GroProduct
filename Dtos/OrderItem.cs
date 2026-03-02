using GroProduct.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroProduct.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }


    }
}
