using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class Tournament
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }

        public string TmtName { get; set; }

        [Required]
        public bool IsStarted { get; set; }

        public ICollection<Group> Groups { get; set; }
        public ICollection<Match> Matches { get; set; }
        public ICollection<Team> Teams { get; set; }

        public Tournament()
        {
            Id = Guid.NewGuid();
            IsStarted = false;
            Groups = new List<Group>();
            Matches = new List<Match>();
            Teams = new List<Team>();
        }

        public Tournament(string name):this()
        {
            TmtName = name;
        }

    }
}