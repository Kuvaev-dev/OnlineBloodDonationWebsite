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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectUser(ReigstrationMV reigstrationMV)
        {
            if (reigstrationMV.UserTypeID == 2)
            {
                return RedirectToAction("DonorUser");
            }
            else if (reigstrationMV.UserTypeID == 3)
            {
                return RedirectToAction("SeekerUser");
            }
            else if (reigstrationMV.UserTypeID == 4)
            {
                return RedirectToAction("HospitalUser");
            }
            else if (reigstrationMV.UserTypeID == 5)
            {
                return RedirectToAction("BloodBankUser");
            }
            else
            {
                return RedirectToAction("MainHome", "Home");
            }

            var registration = new ReigstrationMV();
            return View(registration);
        }

        public ActionResult HospitalUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HospitalUser(HospitalMV hospitalMV)
        {
            return View();
        }

        public ActionResult DonorUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorUser(DonorMV donorMV)
        {
            return View();
        }

        public ActionResult BloodBankUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodBankUser(BloodBankMV bloodBankMV)
        {
            return View();
        }

        public ActionResult SeekerUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerUser(SeekerMV seekerMV)
        {
            return View();
        }
    }
}