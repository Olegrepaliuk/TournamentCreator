using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentCreator.Models
{
    public interface IRepository
    {
        IEnumerable<Team> Teams { get; }
        IEnumerable<GroupsTeams> GroupsTeams { get; }
        IEnumerable<Group> Groups { get; }
        IEnumerable<Tournament> Tournaments { get; }

        void DeleteTeamFromGroup(Guid groupId, Guid teamId);
        void AddTournament(Tournament tmt);
        void AddTeam(Team team);
        void EditTeam(Team team, Guid teamToEditId);
    }
}
