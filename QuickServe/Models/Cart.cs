using System.ComponentModel.DataAnnotations;

namespace QuickServe.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }
}
