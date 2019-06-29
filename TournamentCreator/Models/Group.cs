using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public enum GroupType { UCLType, WCType };

    public class Group
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }

        public string GName { get; set; }

        [Required]
        public GroupType GroupType { get; set; }
        
        [Required]
        public int TeamsNum { get; set; }

        public ICollection<Team> Teams { get; set; }
        public ICollection<Match> Matches { get; set; }

        public Group()
        {
            Teams = new List<Team>();
            Matches = new List<Match>();
            Id = Guid.NewGuid();
            TeamsNum = 4;
            GroupType = GroupType.UCLType;
        }

        public Group(string groupName):this()
        {
            GName = groupName;
        }
        public Group(int teamsNum, GroupType groupType):this()
        {
            TeamsNum = teamsNum;
            GroupType = groupType;
        }

        public void GenerateCalendar()
        {

        }

        public void GenerateStandartUCL()
        {
            
        }
    }
}