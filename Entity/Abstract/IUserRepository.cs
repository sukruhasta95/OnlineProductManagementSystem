using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);

        Task<string?> RegisterAsync(string username, string password, string role);
    }
}
