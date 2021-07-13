using SandboxGame.ConsoleApp.Infrastructure;
using SandboxGame.ConsoleApp.Service.Interfaces;
using SandboxGame.Data.Data;
using System;
using System.Text;
using System.Threading;

namespace SandboxGame.ConsoleApp.Views
{
    public class HeroView
    {
        private readonly UserView userView;
        private StringBuilder builder = new();
        private readonly SandBoxGameDbContext context;
        private readonly IHeroService heroService;
        public HeroView(SandBoxGameDbContext context, IHeroService heroService, UserView userView)
        {
            this.context = context;
            this.heroService = heroService;
            this.userView = userView;
        }
        public void RenderHeroMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Create hero");
                Console.WriteLine("2.List all your hero ");
                Console.WriteLine("3. Details");
                Console.WriteLine("4. Remove hero");
                Console.WriteLine("5. Go back");
                Console.WriteLine();
                Console.Write("Enter command: ");
                string command = Console.ReadLine();
                //var userId = GlobalUser.currUser.Id;

                Thread.Sleep(500);
                Console.Clear();
                switch (command)
                {
                    case "1":
                        RenderCreateHeroMenu();
                        break;
                    case "2":
                        Console.WriteLine(PrintAllheroByUsers());
                        break;
                    case "3":
                        Console.WriteLine(PrintDetailsAboutHeroesByUser());
                        break;
                    case "4":
                        RenderDeleteHeroMenu();
                        break;
                    case "5":
                        userView.RenderUserMenu();
                        break;
                    default:
                        throw new InvalidOperationException("Invalid option.");
                }
                this.builder = new();
                Console.WriteLine("Tab some button to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private void RenderCreateHeroMenu()
        {
            Console.Clear();

            Console.WriteLine("Enter character name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter attack points:");
            var attacPoints = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter defence points:");
            var defencePoints = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter health points:");
            var healthPoints = int.Parse(Console.ReadLine());
            heroService.CreateHero(name, attacPoints,defencePoints,healthPoints);
        }
        public string PrintAllheroByUsers()
        {
            if (!HasUserHeroes())
            {
                builder.AppendLine("This user doesn't heroes.");
            }
            else
            {
                foreach (var hero in this.heroService.GetAllUserHeroes())
                {
                    this.builder.AppendLine($"--Id:{hero.Id} -- Name:{hero.CharacterName}");
                }
            }
            return this.builder.ToString();
        }
        private string PrintDetailsAboutHeroesByUser()
        {
            if (!HasUserHeroes())
            {
                builder.AppendLine("This user doesn't heroes.");

            }
            else
            {
                foreach (var hero in this.heroService.GetAllUserHeroes())
                {
                    this.builder
                        .AppendLine($"--Id:{hero.Id} -- Name:{hero.CharacterName}")
                        .AppendLine($"Attack Points: {hero.AttackPoints}, CreatedOn: {hero.DateCreated.ToString()}");
                }
            }
            return this.builder.ToString();
        }
        private bool HasUserHeroes()
            => heroService.GetAllUserHeroes().Count > 0;
        private void RenderDeleteHeroMenu()
        {
            Console.WriteLine("Enter hero name");
            var name = Console.ReadLine();
            heroService.DeleteHero(name);
        }
    }
}
