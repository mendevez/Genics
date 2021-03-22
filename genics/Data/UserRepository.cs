using genics.Dtos;
using genics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace genics.Data
{
    public class UserRepository : IUserRepository
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterNewUser(RegisterDto model)
        {
            if (model == null)
            {
                throw new NullReferenceException("No data provided");
            }

            var newUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname

            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                return new AuthResponse { Message="User Created Successfully", Success = true };
            }

            return new AuthResponse { Message = "Unable to register new user", Success = false, Errors = result.Errors.Select(e => e.Description)};
        }

        public async Task<AuthResponse> LoginUser(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return new AuthResponse
                {
                    Message = "User does not exist",
                    Success = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!result)
            {
                return new AuthResponse
                {
                    Message = "Password invalid",
                    Success = false
                };
            }
            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            // Get signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

            // Generate token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponse
            {
                Message = "Logged in successfully",
                Token = tokenString,
                Success = true
            };
        }
    }
}
