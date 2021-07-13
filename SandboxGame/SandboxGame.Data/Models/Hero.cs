using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SandboxGame.Data.Models
{
    public class Hero
    {
        public int Id { get; set; }
        [Required]
        public string CharacterName { get; set; }
        public int AttackPoints { get; set; }
        public int DefencePoints { get; set; }
        public int HealthPoints { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Battle Battle { get; set; }
    }
}
