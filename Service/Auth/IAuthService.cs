using BlogBackEnd.DTO.Auth;

namespace BlogBackEnd.Service.Auth
{
    public interface IAuthService
    {
        Task<AuthDTO> RegisterAsync(RegisterDTO model);
        Task<AuthDTO> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        
     }
 }