using System;
using System.Collections.Generic;
using System.Linq;
using SandboxGame.ConsoleApp.Infrastructure;
using SandboxGame.ConsoleApp.Service.Interfaces;
using SandboxGame.ConsoleApp.Views;
using SandboxGame.Data.Data;
using SandboxGame.Data.Models;

namespace SandboxGame.ConsoleApp.Service
{
    public class HeroService : IHeroService
    {
        private readonly UserView userView;
        private readonly HeroView heroView;
        private readonly SandBoxGameDbContext context;
        public HeroService(SandBoxGameDbContext context, UserView userView)
        {
            this.context = context;
            this.userView = userView;
            this.heroView = new HeroView(context, this, userView);
        }
        public void CreateHero(string name, int attackPoints,int defencePoints,int healthPoints)
        {
            if (name == null || attackPoints == 0)
            {
                Console.WriteLine("Some field is empty.");
                heroView.RenderHeroMenu();
            }
            if (IsHeroValid(name))
            {
                Console.WriteLine("Invalid hero.");
                heroView.RenderHeroMenu();
            }
            var hero = new Hero
            {
                CharacterName = name,
                AttackPoints = attackPoints,
                DefencePoints = defencePoints,
                HealthPoints = healthPoints,
                DateCreated = DateTime.UtcNow,
                UserId = GlobalUser.currUser.Id
            };

            this.context.Heroes.Add(hero);

            this.context.SaveChanges();

            Console.WriteLine("Ok!");
        }

        public void DeleteHero(string heroName)
        {
            var hero = this.context.Heroes
                .FirstOrDefault(h => h.CharacterName == heroName
                && h.UserId == GlobalUser.currUser.Id);

            if (hero == null)
            {
                Console.WriteLine("Invalid hero");
            }
            GlobalUser.currUser.Heros.Remove(hero);

            this.context.SaveChanges();
            Console.WriteLine($"Hero: {hero.CharacterName} was removed.");
        }

        public ICollection<Hero> GetAllUserHeroes()
            => this.context.Heroes
            .Where(h => h.UserId == GlobalUser.currUser.Id)
            .ToList();

        public Hero GetHeroByName(string name)
        {
            var hero = this.context.Heroes
                .FirstOrDefault(h => h.CharacterName == name);
            
            return hero;
        }

        public bool IsHeroValid(string name)
            => this.context.Heroes.Any(h => h.CharacterName == name);

        public void Attack(Hero hero1,Hero hero2)
        {
            if (hero2.DefencePoints - hero1.AttackPoints <=0)
            {
                hero2.DefencePoints = 0;
                hero2.HealthPoints -= (hero1.AttackPoints - hero2.DefencePoints);
                
            }
            else
            {
                hero2.DefencePoints -= hero1.AttackPoints;
            }
        }
    }
}
