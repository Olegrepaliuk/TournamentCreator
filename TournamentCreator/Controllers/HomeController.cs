﻿using System;
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
            TeamContext db = new TeamContext("TeamContext");
            List<Team> myTeams = db.Teams.ToList();
            List<Group> myGroups = db.Groups.ToList();


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


            List<Tournament> tournaments = db.Tournaments.ToList();
            Tournament t = new Tournament("TestTmt");
            db.Tournaments.Add(t);
            db.SaveChanges();
            ViewBag.Tournaments = tournaments;


            int a = 0;

            return View();
        }

        public ActionResult TmtSettings(Guid tmtId)
        {
            TeamContext db = new TeamContext("TeamContext");
            List<Tournament> myTournaments = db.Tournaments.ToList();
            var foundTournament = myTournaments.Where(t => t.Id == tmtId).First();
            ViewBag.GroupsOfTmt = foundTournament.Groups.ToList();
            ViewBag.FoundTmt = foundTournament;
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
    }
}