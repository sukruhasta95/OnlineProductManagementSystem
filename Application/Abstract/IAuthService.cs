using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password);
        Task<string?> RegisterAsync(string username, string password, string role);
    }
}
