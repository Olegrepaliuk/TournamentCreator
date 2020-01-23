using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TournamentCreator.Models
{
    public class CRUDRepository : IRepository
    {
        private TeamContext db;

        public CRUDRepository()
        {
            db = new TeamContext("TmtContext2");
        }

        public IEnumerable<Team> Teams
        {
            get
            {
                return db.Teams;
            }
            
        }

        public IEnumerable<Group> Groups
        {
            get
            {
                return db.Groups;
            }
            
        }

        public IEnumerable<Tournament> Tournaments
        {
            get
            {
                return db.Tournaments;
            }
            
        }
        public IEnumerable<GroupsTeams> GroupsTeams
        {
            get
            {
                return db.GroupsTeams;
            }
            
        }

        public void DeleteTeamFromGroup(Guid groupId, Guid teamId)
        {
            var foundConn = db.GroupsTeams.First(gt => (gt.GroupId == groupId) & (gt.TeamId == teamId));
            if (foundConn != null) db.GroupsTeams.Remove(foundConn);
            db.SaveChangesAsync();
        }

        public void AddTeamToGroup(Guid groupId, Guid teamId)
        {
            db.GroupsTeams.Add(new GroupsTeams(groupId, teamId));
            db.SaveChangesAsync();
        }

        public void AddTournament(Tournament tmt)
        {
            db.Tournaments.Add(tmt);
            db.SaveChangesAsync();
        }

        public void AddTeam(Team team)
        {
            db.Teams.Add(team);
            db.SaveChangesAsync();
        }

        public void EditTeam(Team team, Guid teamToEditId)
        {
            List<Team> myTeams = db.Teams.ToList();
            var foundTeam = myTeams.Where(t => t.Id == teamToEditId).FirstOrDefault();
            if (foundTeam != null)
            {
                foundTeam.ChangeFields(team);
            }
            db.SaveChangesAsync();
        }

        public Tournament FindTournamentById(Guid tmtId)
        {
            return Tournaments.Where(t => t.Id == tmtId).FirstOrDefault();
        }

        public Team FindTeamById(Guid teamId)
        {
            return Teams.Where(t => t.Id == teamId).FirstOrDefault();
        }

        public Group FindGroupById(Guid groupId)
        {
            return Groups.Where(g => g.Id == groupId).FirstOrDefault();
        }

    }
}