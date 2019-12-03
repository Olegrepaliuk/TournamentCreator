using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentCreator.Models
{
    public interface IRepository
    {
        IEnumerable<Team> GetAllTeams();
        IEnumerable<GroupsTeams> GetAllGroupsTeams();
        IEnumerable<Group> GetAllGroups();
        IEnumerable<Tournament> GetAllTournaments();

        void DeleteTeamFromGroup(Guid groupId, Guid teamId);
        void AddTournament(Tournament tmt);
        void AddTeam(Team team);
        void EditTeam(Team team, Guid teamToEditId);
    }
}
