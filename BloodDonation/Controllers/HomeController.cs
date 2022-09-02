using DatabaseLayer;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class HomeController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainHome()
        {
            var registrationMV = new ReigstrationMV();
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut => ut.UserTypeID > 1).ToList(), "UserTypeID", "UserType", "0");
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            return View(registrationMV);
        }
    }
}