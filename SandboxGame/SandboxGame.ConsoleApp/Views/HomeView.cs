using SandboxGame.ConsoleApp.Service;
using SandboxGame.Data.Data;
using System;

namespace SandboxGame.ConsoleApp.Views
{
    public static class HomeView
    {
        //private readonly SandBoxGameDbContext context;
        //public HomeView()
        //{
        //    this.context = new SandBoxGameDbContext();
        //}
        public static void RenderHomeMenu()
        {
            while (true)
            {
                Console.WriteLine("HomeMenu");
                Console.WriteLine("1: Register");
                Console.WriteLine("2: Login");
                Console.WriteLine("3: Exit");

                string command = Console.ReadLine();
                UserView userView = new();
                switch (command)
                {
                    case "1":
                        userView.RenderRegisterMenu();
                        userView.RenderLoginMenu();
                        break;
                    case "2":
                        userView.RenderLoginMenu();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid coomand.");
                        
                }
            }
        }
    }
}

