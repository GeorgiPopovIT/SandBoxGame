using SandboxGame.ConsoleApp.Infrastructure;
using SandboxGame.ConsoleApp.Service;
using SandboxGame.ConsoleApp.Service.Interfaces;
using SandboxGame.Data.Data;
using SandboxGame.Data.Models;
using System;

namespace SandboxGame.ConsoleApp.Views
{
    public class UserView
    {
        private readonly BattleService battleService;
        private readonly BattleView battleView;
        private readonly HeroView heroView;
        private readonly IUserService userService;
        private readonly SandBoxGameDbContext context;
        private readonly IHeroService heroService;
        public UserView()
        {
            this.context = new SandBoxGameDbContext();
            this.userService = new UserService(context);
            this.heroService = new HeroService(context, this);
            this.battleService = new BattleService(context,this);
            this.heroView = new HeroView(context, heroService, this);
            this.battleView = new BattleView(battleService, this.heroView);
        }
        public void RenderUserMenu()
        {
            while (true)
            {
                Console.WriteLine("Users");
                Console.WriteLine("1: List of users");
                Console.WriteLine("2: Go to Heroes Menu");
                Console.WriteLine("3: Battles");
                Console.WriteLine("4: Log out");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        foreach (var user in userService.ListUsers())
                        {
                            Console.WriteLine($"Id:{user.Id} - {user}");
                        }
                        break;
                    case "2":
                        Console.Clear();
                        heroView.RenderHeroMenu();
                        break;
                    case "3":
                        battleView.RenderBattleMenu();
                        break;
                    case "4":
                        GlobalUser.currUser = null;
                        HomeView.RenderHomeMenu();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press any button.");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void RenderRegisterMenu()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            userService.CreateUser(username, password);

            Console.Clear();
        }
        public void RenderLoginMenu()
        {
            Console.WriteLine("--Please Login.");

            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter confirm password:");
            string confPassword = Console.ReadLine();

            userService.LoginUser(username, password, confPassword);

            Console.Clear();

            RenderUserMenu();
        }
       
    }
}
