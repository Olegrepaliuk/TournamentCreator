using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public enum GroupType { UCLType, WCType };

    public class Group
    {
        public Guid Id { get; private set; }

        public string GName { get; set; }
        public GroupType GroupType { get; set; }
        
        public int TeamsNum { get; set; }
        public List<Team> Teams { get; set; }

        public List<Match> Matches { get; set; }
        public Group()
        {
            Teams = new List<Team>();
            Matches = new List<Match>();
            Id = Guid.NewGuid();
            TeamsNum = 4;
            GroupType = GroupType.UCLType;
        }

        public Group(int teamsNum, GroupType groupType):this()
        {
            TeamsNum = teamsNum;
            GroupType = GroupType;
        }

        public void GenerateCalendar()
        {

        }

        public void GenerateStandartUCL()
        {
            
        }
    }
}