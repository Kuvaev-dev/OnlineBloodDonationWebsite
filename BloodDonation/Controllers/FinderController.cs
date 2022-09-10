using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class FinderController : Controller
    {
        public ActionResult FinderDonors()
        {
            return View();
        }
    }
}