using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using stock_portfolio_server.Models;

namespace stock_portfolio_server.services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Register(string username, string password);
        IEnumerable<User> GetAll();
        string GetUserId(ClaimsPrincipal user);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            await _userManager.CheckPasswordAsync(user, password);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public async Task<User> Register(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
                throw new Exception("User already exists");
            
            user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Errors != null)
            {
                var errorString = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errorString.Append(error.Description);
                }

                throw new Exception(errorString.ToString());
            }

            return user;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            var claimsIdentity = user.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }



    }
}