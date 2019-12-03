﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TournamentCreator.Models;
using Microsoft.Security.Application;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace TournamentCreator.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;   
        public HomeController()
        {
            repo = new CRUDRepository();
        }


        public ActionResult Index()
        {
            //to master
            List<Team> myTeams = repo.GetAllTeams().ToList();
            List<Group> myGroups = repo.GetAllGroups().ToList();
            List<GroupsTeams> groupsTeams = repo.GetAllGroupsTeams().ToList();


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
            List<Tournament> tournaments2 = repo.GetAllTournaments().ToList();
            ViewBag.Tournaments = tournaments2;


            int a = 1;

            return View();

        }

        public async Task<ActionResult> TmtSettings(Guid tmtId)
        {
            //TeamContext db = new TeamContext("TmtContext2");
            List<Tournament> myTournaments = repo.GetAllTournaments().ToList();
            Group.Groups = repo.GetAllGroups().ToDictionary(g => g.Id);
            Team.Teams = repo.GetAllTeams().ToDictionary(t => t.Id);
            GroupsTeams.GroupTeam = repo.GetAllGroupsTeams().ToList();

            var foundTournament = myTournaments.Where(t => t.Id == tmtId).FirstOrDefault();
            if(foundTournament != null)
            {
                if(foundTournament.IsStarted)
                {
                    return RedirectToAction("Tournament", "Home", new { tournamentId = foundTournament.Id });
                }
            }
            ViewBag.GroupsOfTmt = foundTournament.Groups.OrderBy(g => g.GName).ToList();
            ViewBag.FoundTmt = foundTournament;
            return View();
        }

        public ActionResult DelTeamFromGroup(Guid tournamentId, Guid teamId, Guid groupId)
        {
            //TeamContext db = new TeamContext("TmtContext2");
            List<Group> myGroups = repo.GetAllGroups().ToList();
            var foundGroup = myGroups.Where(t => t.Id == groupId).FirstOrDefault();
            //var foundTeam = foundGroup.Teams.Where(t => t.Id == teamId).First();
            //foundGroup.Teams.Remove(foundTeam);
            GroupsTeams.GroupTeam.RemoveAll(gt => (gt.GroupId == groupId) & (gt.TeamId == teamId));
            //var foundConn = db.GroupsTeams.First(gt => (gt.GroupId == groupId) & (gt.TeamId == teamId));
            //if (foundConn != null) db.GroupsTeams.Remove(foundConn);
            //db.SaveChanges();
            repo.DeleteTeamFromGroup(groupId, teamId);
            return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId});
        }

        [HttpGet]
        public ActionResult EditTeam(Guid tmtId, Guid teamId)
        {
            Team foundTeam = null;
            Team.Teams = repo.GetAllTeams().ToDictionary(t => t.Id);
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
                //
                //List<Team> myTeams = db.Teams.ToList();
                //var foundTeam = myTeams.Where(t => t.Id == needTeamId).FirstOrDefault();
                //if(foundTeam != null)
                //{
                //    foundTeam.ChangeFields(teamToEdit);
                //}
                //db.SaveChanges();
                //

                repo.EditTeam(teamToEdit, needTeamId);

                return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId });
                //return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId});
            }
            return View();
            
        }

        public ActionResult AddTeam(Guid tmtId, Guid groupId)
        {
            var tournament = FindTournamentById(tmtId);
            var group = FindGroupById(groupId);
            ViewBag.Tournament = tournament;
            ViewBag.GrpId = groupId;
            List<Team> availableTeams = repo.GetAllTeams().ToList();
            GroupsTeams.GroupTeam = repo.GetAllGroupsTeams().ToList();
            foreach (GroupsTeams gt in GroupsTeams.GroupTeam)
            {
                if(tournament.Groups.Where(g => g.Id == gt.Group.Id).FirstOrDefault() != null)
                {
                    availableTeams.RemoveAll(t => t.Id == gt.Team.Id);
                }
                
            }
            ViewBag.AvailableTeams = availableTeams.OrderBy(t => t.TName);
            return View();
        }

        public ActionResult CreateTeam(Guid tmtId, Guid grpId)
        {
            ViewBag.TmtId = tmtId;
            ViewBag.GrpId = grpId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam(Team team, Guid trnmtId)
        {
            if (ModelState.IsValid)
            {
                //
                //List<Team> myTeams = db.Teams.ToList();
                //myTeams.Add(team);
                //db.Teams.Add(team);
                //await Task.Run(()=>db.SaveChanges());
                //
                return RedirectToAction("TmtSettings", "Home", new { tmtId = trnmtId });
            }
            return View();
        }

        public ActionResult Tournament(Guid tournamentId)
        {
            var tournament = FindTournamentById(tournamentId);
            foreach(Group g in tournament.Groups)
            {
                if(g.TeamsNum != GetGroupConnections(g).Count())
                {
                    return RedirectToAction("TmtSettings", "Home", new { tmtId = tournamentId });
                }
            }
            return View();
        }

        public ActionResult CreateTournament()
        {
            return View("PtAddTournament");
        }

        [HttpPost]
        public async Task<ActionResult> AddTournament(Tournament tmt)
        {
            if (ModelState.IsValid)
            {
                if(tmt.Groups == null)
                {
                    tmt.Groups = new List<Group>();
                }
                if(tmt.Groups.Count == 0)
                {
                    tmt.CreateGroups();
                }
                //
                //List<Tournament> myTournaments = db.Tournaments.ToList();
                //myTournaments.Add(tmt);
                //db.Tournaments.Add(tmt);
                //await Task.Run(() => db.SaveChanges());
                //

                repo.AddTournament(tmt);

                return RedirectToAction("Index", "Home");
            }
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
            List<Tournament> myTournaments = repo.GetAllTournaments().ToList();
            var foundTournament = myTournaments.Where(t => t.Id == tmtId).First();
            return foundTournament;
        }

        private Team FindTeamById(Guid teamId)
        {
            List<Team> myTeams = repo.GetAllTeams().ToList();
            var foundTeam = myTeams.Where(t => t.Id == teamId).First();
            return foundTeam;
        }

        private Group FindGroupById(Guid groupId)
        {
            List<Group> myGroups = repo.GetAllGroups().ToList();
            var foundGroup = myGroups.Where(g => g.Id == groupId).First();
            return foundGroup;
        }

        private List<GroupsTeams> GetGroupConnections(Group group)
        {
            List<GroupsTeams> gt = repo.GetAllGroupsTeams().ToList();
            return gt.Where(gts => gts.Group.Id == group.Id).ToList();
        }

        public ActionResult TeamSearch(string name)
        {
            List<Team> searchedTeams = new List<Team>();
            List<Team> teams = repo.GetAllTeams().ToList();
            searchedTeams = teams.Where(t => t.TName.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
            return PartialView("PtTeamsTable", searchedTeams);
        }

    }
}