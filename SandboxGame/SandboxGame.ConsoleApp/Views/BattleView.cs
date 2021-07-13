using System;
using SandboxGame.ConsoleApp.Service;

namespace SandboxGame.ConsoleApp.Views
{
    public class BattleView
    {
        private readonly HeroView heroView;
        private readonly BattleService battleService;
        public BattleView(BattleService battleService, HeroView heroView)
        {
            this.battleService = battleService;
            this.heroView = heroView;
        }
        public void RenderBattleMenu()
        {
            Console.WriteLine(heroView.PrintAllheroByUsers());
            Console.WriteLine(new string('-', 36));

            Console.WriteLine("Enter hero1 name:");
            var hero1Name = Console.ReadLine();
            Console.WriteLine("Enter hero2 name:");
            var hero2Name = Console.ReadLine();

            Console.WriteLine(this.battleService
                .SimulateBattle(hero1Name, hero2Name));

        }
    }
}
