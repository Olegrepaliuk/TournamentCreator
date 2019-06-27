using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class Tournament
    {
        public Guid Id { get; private set; }
        public string TmtName { get; set; }
        public bool IsStarted { get; set; }

        public List<Group> Groups { get; set; }
        public List<Match> Matches { get; set; }
        public List<Team> Teams { get; set; }

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