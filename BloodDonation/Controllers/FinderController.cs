using BloodDonation.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class FinderController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult FinderDonors()
        {
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");

            return View(new FinderMV());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinderDonors(FinderMV finderMV)
        {
            var setDate = DateTime.Now.AddDays(-120);

            var donors = DB.DonorTables.Where(d => d.BloodGroupID == finderMV.BloodGroupID &&
                                                   d.LastDonationDate < setDate).ToList();
            foreach (var donor in donors)
            {
                var user = DB.UserTables.Find(donor.UserID);
                if (user.AccountStatusID == 2)  // Approved
                {
                    var addDonor = new FinderSearchResultMV();
                    addDonor.BloodGroup = donor.BloodGroupTable.BloodGroup;
                    addDonor.BloodGroupID = donor.BloodGroupID;
                    addDonor.ContactNo = donor.ContactNo;
                    addDonor.DonorID = donor.DonorID;
                    addDonor.FullName = donor.FullName;
                    addDonor.UserType = "Person";
                    addDonor.UserTypeID = user.UserTypeID;
                    finderMV.SearchResult.Add(addDonor);
                }
            }

            var bloodBanks = DB.BloodBankStockTables.Where(d => d.BloodGroupID == finderMV.BloodGroupID &&
                                                                d.Quantity > 0).ToList();
            foreach (var bloodBank in bloodBanks)
            {
                var findBloodBank = DB.BloodBankTables.Find(bloodBank.BloodBankID);
                var user = DB.UserTables.Find(findBloodBank.UserID);

                if (user.AccountStatusID == 2)  // Approved
                {
                    var addDonor = new FinderSearchResultMV();
                    addDonor.BloodGroup = bloodBank.BloodGroupTable.BloodGroup;
                    addDonor.BloodGroupID = bloodBank.BloodGroupID;
                    addDonor.ContactNo = bloodBank.BloodBankTable.PhoneNo;
                    addDonor.DonorID = bloodBank.BloodBankID;
                    addDonor.FullName = bloodBank.BloodBankTable.BloodBankName;
                    addDonor.UserType = "Blood Bank";
                    addDonor.UserTypeID = user.UserTypeID;
                    finderMV.SearchResult.Add(addDonor);
                }
            }

            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", finderMV.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", finderMV.CityID);

            return View(finderMV);
        }
    }
}