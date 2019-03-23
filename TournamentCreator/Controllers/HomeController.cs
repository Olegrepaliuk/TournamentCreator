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

            Team t1 = new Team();
            t1.TName = "Test1Name";
            t1.Country = "TestCountry";
            t1.City = "TestCity";
            db.Teams.Add(t1);
            db.SaveChanges();

            int a = 0;
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