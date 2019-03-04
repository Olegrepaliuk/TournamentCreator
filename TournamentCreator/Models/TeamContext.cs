using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class TeamContext : DbContext
    {
        public TeamContext() :base("Data Source=WIN-8AVFSJ5T7N2;Initial Catalog=TournamentDB;Integrated Security=True")
        {

        }

        public DbSet<Team> Teams { get; set; }
    }
}