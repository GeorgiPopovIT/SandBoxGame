using SandboxGame.Data.Models;
using System.Collections.Generic;

namespace SandboxGame.ConsoleApp.Service.Interfaces
{
    public interface IHeroService
    {
        void CreateHero(string name, int attackPoints, int defencePoints, int healthPoints);
        ICollection<Hero> GetAllUserHeroes();
        void DeleteHero(string heroName);
        bool IsHeroValid(string name);
        Hero GetHeroByName(string name);
        void Attack(Hero hero1, Hero hero2);
    }
}
