using SandboxGame.ConsoleApp.Infrastructure;
using SandboxGame.ConsoleApp.Service.Interfaces;
using SandboxGame.ConsoleApp.Views;
using SandboxGame.Data.Data;
using SandboxGame.Data.Models;
using System;
using System.Linq;

namespace SandboxGame.ConsoleApp.Service
{
    public class BattleService
    {
        private readonly UserView userView;
        private readonly IHeroService heroService;
        private readonly SandBoxGameDbContext context;
        public BattleService(SandBoxGameDbContext context,UserView userView)
        {
            this.context = context;
            this.userView = userView;
            this.heroService = new HeroService(context,this.userView);
        }
        public string SimulateBattle(string hero1Name, string hero2Name)
        {
            string winnerString;
            var battle = new Battle();

            var hero1 = this.heroService.GetHeroByName(hero1Name);
            var hero2 = this.heroService.GetHeroByName(hero2Name);
            if (hero1 == null || hero2 == null)
            {
                return "Try again.Some name is invalid.";
            }
            while (hero1.HealthPoints >=0 || hero2.HealthPoints >= 0)
            {
                this.heroService.Attack(hero1, hero2);
                this.heroService.Attack(hero2, hero1);
            }

            if (hero1.HealthPoints <= 0)
            {
                battle.WinnerId = hero2.Id;
                battle.LoserId = hero1.Id;

                winnerString =  $"Winner: {hero2.CharacterName}; Loser {hero1.CharacterName}";
            }

            battle.WinnerId = hero1.Id;
            battle.LoserId = hero2.Id;
            winnerString = $"Winner: {hero1.CharacterName}; Loser {hero2.CharacterName}";

            battle.CreatedDate = DateTime.UtcNow;
            battle.UserId = GlobalUser.currUser.Id;

            this.context.Battles.Add(battle);
            this.context.SaveChanges();

            return winnerString;
        }
    }
}
