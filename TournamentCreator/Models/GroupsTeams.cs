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

        public int GamesCompleted { get; private set; }
        public int Points { get; private set; }

        public int Scored { get; private set; }

        public int Missed { get; private set; }

        public static List<GroupsTeams> GroupTeam = new List<GroupsTeams>();

        public Guid GroupId { get; private set; }
        public Guid TeamId { get; private set;}

        [NotMapped]
        public Group Group
        {
            get { return Group.Groups[GroupId]; }
            set { GroupId = value.Id; }
        }

        [NotMapped]
        public Team Team
        {
            get { return Team.Teams[TeamId]; }
            set { TeamId = value.Id; }
        }

        public GroupsTeams()
        {
            Id = Guid.NewGuid();
        }

        public GroupsTeams(Guid groupId, Guid teamId)
        {
            GroupId = groupId;
            TeamId = teamId;
        }

        public void UpdateStats()
        {
            int gamesCompleted = 0;
            int points = 0;
            int scored = 0;
            int missed = 0;

            if (Team == null || Group == null) return;
            List<Match> teamMatches = Group.Matches.Where(m => ((m.HomeTeam == Team) || (m.AwayTeam == Team))).ToList();
            foreach(Match match in teamMatches)
            {
                if (match.Completed)
                {
                    gamesCompleted++;
                    if (match.Winner == Team) points += 3;
                    if (match.Winner == null) points += 1;

                    if(Team == match.HomeTeam)
                    {
                        scored += match.HomeScore;
                        missed += match.AwayScore;  
                    }
                    else
                    {
                        scored += match.AwayScore;
                        missed += match.HomeScore;
                    }
                }
            }
        }

        public void UpdateStats(List<Team> teams)
        {

        }

        public void UpdateStats(Team team)
        {

        }
    }
}