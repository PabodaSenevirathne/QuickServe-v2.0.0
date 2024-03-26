using System.ComponentModel.DataAnnotations;

namespace QuickServe.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }
}
