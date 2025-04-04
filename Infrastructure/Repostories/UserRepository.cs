using BCrypt.Net;
using Entities.Abstract;
using Entities.Concrete;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repostories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<string?> RegisterAsync(string username, string password, string role)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                return "Bu kullanıcı adı zaten alınmış!";
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password= BCrypt.Net.BCrypt.HashPassword(password),
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "Kullanıcı başarıyla kaydedildi!";
        }
    }
}
