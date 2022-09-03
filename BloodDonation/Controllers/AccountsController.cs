﻿using DatabaseLayer;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class AccountsController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult AllNewUserRequests()
        {
            var users = DB.UserTables.Where(u => u.AccountStatusID == 1).ToList();
            return View(users);
        }

        public ActionResult UserDetails(int? id)
        {
            var user = DB.UserTables.Find(id);
            return View(user);
        }

        public ActionResult UserApproved(int? id)
        {
            return View();
        }

        public ActionResult UserRejected()
        {
            return View();
        }
    }
}