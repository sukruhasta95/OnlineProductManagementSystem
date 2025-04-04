using Application.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class AutManager : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AutManager(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.Password)) { return null; }
            return GenerateJwtToken(user);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var ss = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
                var key = new SymmetricSecurityKey(ss);
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                 {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Name, user.Username),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new (JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new(ClaimTypes.Role,user.Role)
                 };


                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(8),
                    signingCredentials: credentials
                );
                Console.WriteLine($"alınan token: {new JwtSecurityTokenHandler().WriteToken(token)}");
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JWT oluşturma hatası: {ex.Message}");
                throw new Exception("JWT oluşturulurken hata meydana geldi.");
            }

        }

        public Task<string?> RegisterAsync(string username, string password, string role)
        {
            return _userRepository.RegisterAsync(username, password, role);
        }
    }
}
