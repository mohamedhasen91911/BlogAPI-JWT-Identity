using System.ComponentModel.DataAnnotations;

namespace BlogBackEnd.DTO.Auth
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }
     }
 }