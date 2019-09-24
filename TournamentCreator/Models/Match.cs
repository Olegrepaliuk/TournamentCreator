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

        public bool IsCompleted { get; set; }


        [Required]
        public int HomeScore { get; set; }

        [Required]
        public int AwayScore { get; set; }

        public Team Winner
        {
            get
            {
                //if ((HomeScore > AwayScore)&(IsCompleted)) return HomeTeam;
                //if ((HomeScore < AwayScore) & (IsCompleted)) return AwayTeam;
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