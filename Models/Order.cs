using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GroProduct.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        // Foreign Key column
        [Required]
        public int UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }


    }
}