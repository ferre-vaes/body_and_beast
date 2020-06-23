using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BeastAndBody.API.Models;
using BeastAndBody.API.Settings;
using BeastAndBody.Data.Models;
using BeastAndBody.Data.Models.Auth;
using BeastAndBody.Data.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BeastAndBody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IOptions<TokenSettings> _tokenSettings;

        public AuthenticationController(RoleManager<Role> roleManager, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher, IOptions<TokenSettings> tokenSettings)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //TODO: add captcha validation to prevent registrations of bots

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Type = model.Type
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var role = (model.Type == UserType.Coach) ? Role.Constants.Coach : Role.Constants.Client;
                await EnsureRoleExist(role);
                await _userManager.AddToRoleAsync(user, role);

                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) !=
                PasswordVerificationResult.Success)
            {
                return Unauthorized();
            }

            var token = await CreateJwtToken(user);
            return Ok(token);
        }

        private async Task EnsureRoleExist(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new Role
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
            }
        }

        private async Task<string> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var allClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }.Union(userClaims).ToList();

            var userRoles = await _userManager.GetRolesAsync(user);

            allClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var keyBytes = Encoding.UTF8.GetBytes(_tokenSettings.Value.Key);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Value.Issuer,
                audience: _tokenSettings.Value.Audience,
                claims: allClaims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.Value.ExpirationTimeInMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
