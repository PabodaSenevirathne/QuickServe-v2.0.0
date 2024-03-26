using System.ComponentModel.DataAnnotations;

namespace QuickServe.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? ShippingAddress { get; set; }
    }
}
