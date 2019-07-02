using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        public static Dictionary<Guid, Group> Groups = new Dictionary<Guid, Group>();
        //public ICollection<Team> Teams { get; set; }
        public ICollection<Match> Matches { get; set; }

        [NotMapped]
        public List<GroupsTeams> GroupTeam
        {
            get
            {
                List<GroupsTeams> res = new List<GroupsTeams>();
                foreach (GroupsTeams gt in GroupsTeams.GroupTeam)
                    if (gt.Group == this)
                        res.Add(gt);
                return res;
            }
        }

        [NotMapped]
        public List<Team> Teams
        {
            get
            {
                List<Team> res = new List<Team>();
                foreach (GroupsTeams gt in GroupsTeams.GroupTeam)
                    if (gt.Group == this)
                        res.Add(gt.Team);
                return res;
            }
        }

        public Group()
        {  
            //Teams = new List<Team>();
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