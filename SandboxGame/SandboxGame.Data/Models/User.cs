using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SandboxGame.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        public ICollection<Hero> Heros { get; set; } = new HashSet<Hero>();
    }
}
