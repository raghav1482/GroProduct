using System.Text.Json.Serialization;

namespace GroProduct.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public Guid OrderId { get; set; }

        // prevent cycles when serializing orders with their items
        [JsonIgnore]
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
