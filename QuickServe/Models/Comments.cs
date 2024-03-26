using System.ComponentModel.DataAnnotations;

namespace QuickServe.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Rating { get; set; }

        public string? Image { get; set; }

        [Required]
        public string? Text { get; set; }
    }
}
