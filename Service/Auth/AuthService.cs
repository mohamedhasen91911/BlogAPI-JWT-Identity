using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogBackEnd.DTO.Auth;
using BlogBackEnd.Helpers;
using BlogBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogBackEnd.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> _userManager, IOptions<JWT> _jwt ,RoleManager<IdentityRole> _roleManager )
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._jwt = _jwt.Value;
        }

        public async Task<AuthDTO> RegisterAsync(RegisterDTO model)
        {
            //Check For Email
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new AuthDTO { Message = "Email Is Already Register" };
            //Check For Username
            if (await _userManager.FindByNameAsync(model.Username) != null)
                return new AuthDTO { Message = "UserName Is Already Register" };

            //Convert From RegisterDto TO ApplicationUser

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var erros = string.Empty;
                foreach (var error in result.Errors)
                {
                    erros += $"{error.Description},";
                }
                return new AuthDTO { Message = erros };
            }

            //Add Register User To Default Role
            await _userManager.AddToRoleAsync(user, "User");

            //Generate Token And Return in 
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthDTO
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };

        }
        
        public async Task<AuthDTO> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthDTO();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                authModel.Message = "Email Or Password Is Incorrect";
                return authModel;
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email Or Password Is Incorrect";
                return authModel;
             }

             //Generate Token And Return it
            var jwtSecurityToken = await CreateJwtToken(user);

            var rolesList = await _userManager.GetRolesAsync(user);


            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.IsAuthenticated = true;
            authModel.Roles = rolesList.ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Username = user.UserName;


            return authModel;

         }

         public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null)
            {
                return "Invalid User ID Or Role";
            }
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                return "Invalid User ID Or Role";
            }

            if (await _userManager.IsInRoleAsync(user, model.Role))
            {
                return "User Already assigned to this rule";
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return string.Empty;
            }

            return "Something Went Wrong";
             
         }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim("uid" , user.Id)
             }.Union(userClaims).Union(roleClaims);


            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.key));
            var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials

            );

            return jwtSecurityToken;
        }

    }
 }