using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TournamentCreator.Models;

namespace TournamentCreator.Controllers
{
    public class HomeController : Controller
    {
        TeamContext db = new TeamContext("TmtContext2");

        public ActionResult Index()
        {
            
            List<Team> myTeams = db.Teams.ToList();
            List<Group> myGroups = db.Groups.ToList();
            List<GroupsTeams> groupsTeams = db.GroupsTeams.ToList();


            //Team t1 = new Team();
            //t1.TName = "Test1Name";
            //t1.Country = "TestCountry";
            //t1.City = "TestCity";
            //db.Teams.Add(t1);
            //db.SaveChanges();

            //Group g1 = new Group();
            //g1.Teams.Add(t1);
            //Group g2 = new Group();
            //g2.Teams.Add(t1);

            //db.Groups.Add(g1);
            //db.Groups.Add(g2);
            //db.SaveChanges();

            /*
            List<Team> myTeams2 = db.Teams.ToList();
            List<Group> myGroups2 = db.Groups.ToList();


            //Team t2 = new Team();
            //Match match1 = new Match();
            //Guid g = match1.Id;


            List<Tournament> tournaments = db.Tournaments.ToList();
            ViewBag.Tournaments = tournaments;

            Team t1 = new Team();
            */

            /*
            Tournament tt = new Tournament("TestTmt");
            db.Tournaments.Add(tt);
            db.SaveChanges();
            */

            /*
            List<Tournament> tournaments = db.Tournaments.ToList();
            var t = tournaments.First();
            Team testTeam = new Team("Barca", "Spain", "Barcelona");
            db.Teams.Add(testTeam);
            db.SaveChanges();

            var g = t.Groups.Where(gr => gr.GName == "Group A").First();
            var gt = new GroupsTeams(g.Id, testTeam.Id);
            GroupsTeams.GroupTeam.Add(gt);
            db.GroupsTeams.Add(gt);
            db.SaveChanges();
            
            */
            List<Tournament> tournaments2 = db.Tournaments.ToList();
            ViewBag.Tournaments = tournaments2;


            int a = 0;

            return View();

        }

        public ActionResult TmtSettings(Guid tmtId)
        {
            //TeamContext db = new TeamContext("TmtContext2");
            List<Tournament> myTournaments = db.Tournaments.ToList();
            Group.Groups = db.Groups.ToDictionary(g => g.Id);
            Team.Teams = db.Teams.ToDictionary(t => t.Id);
            GroupsTeams.GroupTeam = db.GroupsTeams.ToList();

            var foundTournament = myTournaments.Where(t => t.Id == tmtId).First();
            ViewBag.GroupsOfTmt = foundTournament.Groups.ToList();
            ViewBag.FoundTmt = foundTournament;
            return View();
        }

        public ActionResult DelTeamFromGroup(Guid tournamentId, Guid teamId, Guid groupId)
        {
            //TeamContext db = new TeamContext("TmtContext2");
            List<Group> myGroups = db.Groups.ToList();
            var foundGroup = myGroups.Where(t => t.Id == groupId).FirstOrDefault();
            //var foundTeam = foundGroup.Teams.Where(t => t.Id == teamId).First();
            //foundGroup.Teams.Remove(foundTeam);
            GroupsTeams.GroupTeam.RemoveAll(gt => (gt.GroupId ==groupId)&(gt.TeamId == teamId));
            var foundConn = db.GroupsTeams.First(gt => (gt.GroupId == groupId) & (gt.TeamId == teamId));
            if (foundConn != null) db.GroupsTeams.Remove(foundConn);
            db.SaveChanges();
            return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId});
        }

        [HttpGet]
        public ActionResult EditTeam(Guid tmtId, Guid teamId)
        {
            Team foundTeam = null;
            Team.Teams = db.Teams.ToDictionary(t => t.Id);
            if(Team.Teams.ContainsKey(teamId))
            {
                foundTeam = Team.Teams[teamId];
            }
            else
            {
                return View("ErrorPage");
            }
            ViewBag.TeamToEdit = foundTeam;
            ViewBag.TournamentId = tmtId;
            return View(foundTeam);
        }

        [HttpPost]
        public ActionResult EditTeam(Team teamToEdit, Guid needTeamId, Guid tournamentId)
        {
            if (ModelState.IsValid)
            {
                List<Team> myTeams = db.Teams.ToList();
                var foundTeam = myTeams.Where(t => t.Id == needTeamId).FirstOrDefault();
                if(foundTeam != null)
                {
                    foundTeam.ChangeFields(teamToEdit);
                }
                db.SaveChanges();
                return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId });
                //return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId});
            }
            return View("ErrorPage");
            
        }

        public ActionResult AddTeam(Guid tmtId, Guid groupId)
        {
            var tournament = FindTournamentById(tmtId);
            var group = FindGroupById(groupId);
            ViewBag.Tournament = tournament;
            List<Team> availableTeams = db.Teams.ToList();
            GroupsTeams.GroupTeam = db.GroupsTeams.ToList();
            foreach (GroupsTeams gt in GroupsTeams.GroupTeam)
            {
                if(tournament.Groups.Where(g => g.Id == gt.Group.Id).FirstOrDefault() != null)
                {
                    availableTeams.RemoveAll(t => t.Id == gt.Team.Id);
                }
                
            }
            ViewBag.AvailableTeams = availableTeams;
            return View();
        }

        public ActionResult CreateTeam(Guid tmtId, Guid grpId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTeam(Team team, Guid tmtId)
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page."; 
            return View();
        }

        private Tournament FindTournamentById(Guid tmtId)
        {
            List<Tournament> myTournaments = db.Tournaments.ToList();
            var foundTournament = myTournaments.Where(t => t.Id == tmtId).First();
            return foundTournament;
        }

        private Team FindTeamById(Guid teamId)
        {
            List<Team> myTeams = db.Teams.ToList();
            var foundTeam = myTeams.Where(t => t.Id == teamId).First();
            return foundTeam;
        }

        private Group FindGroupById(Guid groupId)
        {
            List<Group> myGroups = db.Groups.ToList();
            var foundGroup = myGroups.Where(g => g.Id == groupId).First();
            return foundGroup;
        }

    }
}