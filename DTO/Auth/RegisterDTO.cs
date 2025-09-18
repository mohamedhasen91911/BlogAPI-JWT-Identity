using System.ComponentModel.DataAnnotations;

namespace BlogBackEnd.DTO.Auth
{
        public class RegisterDTO
    {
        [Required,StringLength(50)]
        public string FirstName { get; set; }
        [Required , StringLength(50)]
        public string LastName { get; set; }
        [Required , StringLength(50)]
        public string Username { get; set; }
        [Required , StringLength(150)]
        public string Email { get; set; }
        [Required , StringLength(265)]
        public string Password { get; set; }
     }

 }