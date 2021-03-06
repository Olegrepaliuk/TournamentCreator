﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class Team : IEquatable<Team>
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "Name is required")]
        public string TName { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        public string Stadium { get; set; }

        public static Dictionary<Guid, Team> Teams = new Dictionary<Guid, Team>();
        //public ICollection<Group> Groups { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }

        [NotMapped]
        public List<GroupsTeams> GroupTeam
        {
            get
            {
                List<GroupsTeams> res = new List<GroupsTeams>();
                foreach (GroupsTeams gt in GroupsTeams.GroupTeam)
                    if (gt.Team == this)
                        res.Add(gt);
                return res;
            }
        }
        [NotMapped]
        public List<Group> Groups
        {
            get
            {
                List<Group> res = new List<Group>();
                foreach (GroupsTeams gt in GroupsTeams.GroupTeam)
                    if (gt.Team == this)
                        res.Add(gt.Group);
                return res;
            }
        }

        public Team()
        {
            Id = Guid.NewGuid();
            //Groups = new List<Group>();
            Tournaments = new List<Tournament>();
        }
        
        public Team(string tName, string country, string city):this()
        {
            TName = tName;
            Country = country;
            City = city;
        }

        public void ChangeFields(Team otherTeam)
        {
            City = otherTeam.City;
            Country = otherTeam.Country;
            Stadium = otherTeam.Stadium;
            TName = otherTeam.TName;
        }

        public bool Equals(Team other)
        { 
            return ((TName == other.TName) && (City == other.City) && (Country == other.Country));
        }
    }
}