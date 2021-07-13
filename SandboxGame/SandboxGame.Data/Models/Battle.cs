using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandboxGame.Data.Models
{
    public class Battle
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int WinnerId { get; set; }
        public int LoserId { get; set; }


        public ICollection<Hero> Heroes { get; set; } = new HashSet<Hero>();
    }
}
