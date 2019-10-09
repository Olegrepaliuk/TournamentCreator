using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class Match
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public bool Completed { get; set; }

        [Required]
        public int HomeScore { get; set; }

        [Required]
        public int AwayScore { get; set; }

        [NotMapped]
        public Team Winner
        {
            get
            {
                if (!Completed) return null;
                if (HomeScore > AwayScore) return HomeTeam;
                if (HomeScore < AwayScore) return AwayTeam;
                return null;
            }
        }

        public Match()
        {
            Id = Guid.NewGuid();
        }

        public Match(Team homeTeam, Team awayTeam):this()
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }
    }
}