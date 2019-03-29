using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class Match
    {
        public Guid Id { get; private set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public Team Winner
        {
            get
            {
                if (HomeScore > AwayScore) return HomeTeam;
                if (HomeScore < AwayScore) return AwayTeam;
                return null;
            }
        }

        public Match()
        {
            Id = Guid.NewGuid();
        }
    }
}