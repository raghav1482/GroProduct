using System.ComponentModel.DataAnnotations;

namespace GroProduct.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; } = 0;

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public decimal Amount { get; set; }


    }
}
