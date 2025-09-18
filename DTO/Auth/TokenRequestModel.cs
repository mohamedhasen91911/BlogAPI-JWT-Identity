using System.ComponentModel.DataAnnotations;

namespace BlogBackEnd.DTO.Auth
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
     }
 }