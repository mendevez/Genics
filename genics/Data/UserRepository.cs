using AutoMapper;
using genics.Dtos;
using genics.Dtos.Users;
using genics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthResponse> RegisterNewUser(RegisterDto model)
        {
            if (model == null)
            {
                throw new NullReferenceException("No data provided");
            }

            var newUser = _mapper.Map<ApplicationUser>(model);
            newUser.UserName = model.Email;

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                return new AuthResponse { User = null, Message="User Created Successfully", Success = true };
            }

            return new AuthResponse { User = null, Message = "Unable to register new user", Success = false, Errors = result.Errors.Select(e => e.Description)};
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
                    User = null,
                    Message = "Password invalid",
                    Success = false
                };
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname)
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
                User = _mapper.Map<UserReadDto>(user),
                Message = "Logged in successfully",
                Token = tokenString,
                Success = true
            };
        }

        public async Task<ApplicationUser> GetUser(string id)
        { 
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
;        }
    }
}
