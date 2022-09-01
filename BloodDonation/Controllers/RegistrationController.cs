using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult UserRegistration()
        {
            var registration = new ReigstrationMV();
            return View(registration);
        }
    }
}