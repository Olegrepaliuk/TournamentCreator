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

        /*
        public static bool operator ==(Group obj1, Group obj2)
        {
            
            if ((obj1.Id == obj2.Id) && (obj1.GName == obj2.GName) && (obj1.GroupType == obj2.GroupType))
                return true;
            return false;
            
        }

        public static bool operator !=(Group obj1, Group obj2)
        {
            
            if ((obj1.Id != obj2.Id) || (obj1.GName != obj2.GName) || (obj1.GroupType != obj2.GroupType))
                return true;
            return false;
            
        }
        

        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Group g = obj as Group; 
            if (g as Group == null)
                return false;

            return g.Id == this.Id && g.GName == this.GName && g.GroupType == this.GroupType;
        }
        */

        public bool FreeSpaceAvailability(List<GroupsTeams> groupsTeams)
        {
            int connNum = 0;
            foreach(GroupsTeams gt in groupsTeams)
            {
                if(gt.Group.Id == Id)
                {
                    connNum++;
                }
            }

            return (connNum <= TeamsNum);
        }

        public bool FreeSpacaAvailability()
        {
            return (GroupTeam.Count() <= TeamsNum);   
        }
        
        public void GenerateCalendar()
        {

        }

        public void GenerateStandartUCL()
        {
            
        }
    }
}