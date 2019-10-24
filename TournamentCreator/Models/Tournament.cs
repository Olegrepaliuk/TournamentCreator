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

        [Required(ErrorMessage = "Name is required")]
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
            CreateGroups();
        }

        public Tournament(string name):this()
        {
            TmtName = name;
            CreateGroups();
        }


        public void CreateGroups()
        {
            Group g;
            int c = 65;
            for (int i = 0; i < 8; i++)
            {
                g = new Group("Group " + Convert.ToChar(c + i));
                this.Groups.Add(g);
            }
        }


    }
}