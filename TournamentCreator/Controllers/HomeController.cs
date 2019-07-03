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
        public ActionResult Index()
        {
            TeamContext db = new TeamContext("TmtContext2");
            List<Team> myTeams = db.Teams.ToList();
            List<Group> myGroups = db.Groups.ToList();


            //new branch
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

            List<Tournament> tournaments = db.Tournaments.ToList();
            var t = tournaments.First();
            Team testTeam = new Team("Barca", "Spain", "Barcelona");
            var g = t.Groups.Where(gr => gr.GName == "Group A").First();
            g.Teams.Add(testTeam);
            
            //db.Tournaments.Add(t);
            db.SaveChanges();
            
            */
            List<Tournament> tournaments2 = db.Tournaments.ToList();
            ViewBag.Tournaments = tournaments2;

            List<Group> myGroups2 = db.Groups.ToList();
            List<Team> myTeams2 = db.Teams.ToList();

            int a = 0;

            return View();
        }

        public ActionResult TmtSettings(Guid tmtId)
        {
            TeamContext db = new TeamContext("TmtContext2");
            List<Tournament> myTournaments = db.Tournaments.ToList();
            List<Group> myGroups = db.Groups.ToList();
            List<Team> myTeams = db.Teams.ToList();
            var foundTournament = myTournaments.Where(t => t.Id == tmtId).First();
            ViewBag.GroupsOfTmt = foundTournament.Groups.ToList();
            ViewBag.FoundTmt = foundTournament;
            return View();
        }

        public ActionResult DelTeamFromGroup(Guid tournamentId, Guid teamId, Guid groupId)
        {
            TeamContext db = new TeamContext("TmtContext2");
            List<Group> myGroups = db.Groups.ToList();
            var foundGroup = myGroups.Where(t => t.Id == teamId).First();
            var foundTeam = foundGroup.Teams.Where(t => t.Id == teamId).First();
            foundGroup.Teams.Remove(foundTeam);
            db.SaveChanges();
            return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId});
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
    }
}