using System.ComponentModel.DataAnnotations;

namespace BlogBackEnd.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

     }
 }