using BlogBackEnd.DTO.Auth;
using BlogBackEnd.Service.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService _authService) { this._authService = _authService; }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated) return BadRequest(result.Message);
            return Ok(result);

        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
            // return Ok(new { token = result.Token, expiresOn = result.ExpiresOn });

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            
            return Ok(model);
            // return Ok(new { token = result.Token, expiresOn = result.ExpiresOn });

        }
       


    }
 }