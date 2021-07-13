using SandboxGame.ConsoleApp.Infrastructure;
using SandboxGame.ConsoleApp.Views;
using SandboxGame.Data.Data;
using SandboxGame.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SandboxGame.ConsoleApp.Service
{
    public class UserService : IUserService
    {
        private readonly SandBoxGameDbContext context;
        public UserService(SandBoxGameDbContext context)
        {
            this.context = context;
        }
        public void CreateUser(string username, string password)
        {
            var hashedPassword = PasswordHasher.ComputeSha256Hash(password);


            var isContainsUser = this.context.Users
                .FirstOrDefault(u => u.Username == username
                && u.UserPassword == hashedPassword);

            if (isContainsUser != null)
            {
                Console.WriteLine("This user is already registered.");
                return;
            }

            var user = new User
            {
                Username = username,
                UserPassword = hashedPassword,
                DateCreated = DateTime.UtcNow
            };

            this.context.Users.Add(user);

            this.context.SaveChanges();
            Console.WriteLine("User added succesfully.");

        }

        public IEnumerable<User> ListUsers()
        {
            var users = this.context.Users
                .OrderBy(u => u)
                .ToList();

            return users;
        }

        public void LoginUser(string username, string password, string confirmPassword)
        {
            var hashedPassword = PasswordHasher.ComputeSha256Hash(password);

            if (password != confirmPassword)
            {
                Console.WriteLine("Password and confirm password differnt ");
                //Console.Clear();
                return;
                
            }
            var user = this.context.Users.FirstOrDefault(u => u.Username == username
            && u.UserPassword == hashedPassword);

            if (user == null)
            {
                Console.WriteLine("Invalid user.Please go to register Menu. ");
                Console.Clear();
                HomeView.RenderHomeMenu();
            }
            else
            {
                GlobalUser.currUser = user;
                Console.WriteLine("Ok!");
                Console.Clear();
            }
        }
    }
}
