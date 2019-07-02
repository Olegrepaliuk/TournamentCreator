using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class TeamContext : DbContext
    {
        //public TeamContext() :base("Data Source=WIN-8AVFSJ5T7N2;Initial Catalog=TournamentDB;Integrated Security=True")
        //{

        //}

        public TeamContext()
        {

        }

        public TeamContext(string connString) :base(connString)
        {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<GroupsTeams> GroupsTeams { get; set; }
    }
}