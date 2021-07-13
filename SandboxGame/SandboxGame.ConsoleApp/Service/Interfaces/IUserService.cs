using SandboxGame.Data.Models;
using System.Collections.Generic;

namespace SandboxGame.ConsoleApp.Service
{
    public interface IUserService
    {
        void CreateUser(string username, string password);

        IEnumerable<User> ListUsers();
        void LoginUser(string username, string password, string confirmPassword);
    }
}
