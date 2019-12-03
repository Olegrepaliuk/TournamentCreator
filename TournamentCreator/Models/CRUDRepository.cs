using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Team> GetAllTeams()
        {
            return db.Teams;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return db.Groups;
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            return db.Tournaments;
        }
        public IEnumerable<GroupsTeams> GetAllGroupsTeams()
        {
            return db.GroupsTeams;
        }

        public async void DeleteTeamFromGroup(Guid groupId, Guid teamId)
        {
            var foundConn = db.GroupsTeams.First(gt => (gt.GroupId == groupId) & (gt.TeamId == teamId));
            if (foundConn != null) db.GroupsTeams.Remove(foundConn);
            await db.SaveChangesAsync();
        }

        public async void AddTournament(Tournament tmt)
        {
            db.Tournaments.Add(tmt);
            await db.SaveChangesAsync();
        }

        public async void AddTeam(Team team)
        {
            db.Teams.Add(team);
            await db.SaveChangesAsync();
        }

        public void EditTeam(Team team, Guid teamToEditId)
        {
            List<Team> myTeams = db.Teams.ToList();
            var foundTeam = myTeams.Where(t => t.Id == teamToEditId).FirstOrDefault();
            if (foundTeam != null)
            {
                foundTeam.ChangeFields(team);
            }
            db.SaveChanges();
        }
    }
}