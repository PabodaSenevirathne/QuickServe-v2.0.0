using System.ComponentModel.DataAnnotations;

namespace QuickServe.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Image { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal ShippingCost { get; set; }
    }
}
