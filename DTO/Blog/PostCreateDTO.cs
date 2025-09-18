using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackEnd.DTO.Blog
{
    public class PostCreateDTO
    {
        [Required , StringLength(150)]
        public string Title { get; set; }
        [Required , StringLength(265)]
        public string Content { get; set; }
     }
 }