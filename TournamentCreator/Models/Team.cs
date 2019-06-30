using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class Team
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }
        [Required]
        public string TName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public string Stadium { get; set; }

        public ICollection<Group> Groups { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }

        public Team()
        {
            Id = Guid.NewGuid();
            Groups = new List<Group>();
            Tournaments = new List<Tournament>();
        }
        
        public Team(string tName, string country, string city):this()
        {
            TName = tName;
            Country = country;
            City = city;
        }
    }
}