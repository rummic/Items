using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Items.API.Services.UsersServices
{
    public class UsersService : IUsersService
    {
        private readonly IConfiguration _config;
        
        public UsersService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Authenticate(string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JwtTokenSecret"]);
            var expiryDate = DateTime.UtcNow.AddDays(1);

            var subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, role) });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expiryDate,
                SigningCredentials = signingCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
