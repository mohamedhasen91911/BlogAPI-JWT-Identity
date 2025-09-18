using System.ComponentModel.DataAnnotations;

namespace BlogBackEnd.DTO.Blog
{
    public class PostReadDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string AuthorName { get; set; }
     }
 }