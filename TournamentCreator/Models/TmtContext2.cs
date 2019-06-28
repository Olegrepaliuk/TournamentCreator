using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class TmtContext2 : DbContext
    {
        public TmtContext2()
        {

        }

        public TmtContext2(string connString) : base(connString)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Match> Mathes { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}