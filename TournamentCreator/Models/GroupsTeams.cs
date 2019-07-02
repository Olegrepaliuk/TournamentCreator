using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    public class GroupsTeams
    {

        public Guid Id { get; set; }

        public GroupsTeams()
        {
            Id = Guid.NewGuid();
        }

        public static List<GroupsTeams> GroupTeam = new List<GroupsTeams>();

        public Guid GroupId { get; private set; }
        public Guid TeamId { get; private set;}

        [NotMapped]
        public Group Group
        {
            get { return Group.Groups[GroupId]; }
            set { GroupId = value.Id; }
        }

        public GroupsTeams(Guid groupId, Guid teamId)
        {
            GroupId = groupId;
            TeamId = teamId;
        }

        [NotMapped]
        public Team Team
        {
            get { return Team.Teams[TeamId]; }
            set { TeamId = value.Id; }
        }
    }
}