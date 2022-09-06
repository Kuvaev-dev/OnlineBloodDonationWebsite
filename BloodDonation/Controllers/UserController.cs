using BloodDonation.Models;
using DatabaseLayer;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class UserController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult UserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var user = DB.UserTables.Find(id);
            return View(user);
        }

        public ActionResult EditUserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var userProfile = new RegistrationMV();
            var user = DB.UserTables.Find(id);

            userProfile.UserTypeID = user.UserTypeID;

            userProfile.User.UserID = user.UserID;
            userProfile.User.UserName = user.UserName;
            userProfile.User.EmailAddress = user.EmailAddress;
            userProfile.User.AccountStatusID = user.AccountStatusID;
            userProfile.User.UserTypeID = user.UserTypeID;
            userProfile.User.Description = user.Description;

            if (user.SeekerTables.Count > 0)
            {
                var seeker = user.SeekerTables.FirstOrDefault();
                userProfile.Seeker.SeekerID = seeker.SeekerID;
                userProfile.Seeker.FullName = seeker.FullName;
                userProfile.Seeker.Age = seeker.Age;
                userProfile.Seeker.CityID = seeker.CityID;
                userProfile.Seeker.BloodGroupID = seeker.BloodGroupID;
                userProfile.Seeker.ContactNo = seeker.ContactNo;
                userProfile.Seeker.CNIC = seeker.CNIC;
                userProfile.Seeker.GenderID = seeker.GenderID;
                userProfile.Seeker.Address = seeker.Address;
                userProfile.Seeker.UserID = seeker.UserID;

                userProfile.ContactNo = seeker.ContactNo;
                userProfile.CityID = seeker.CityID;
                userProfile.BloodGroupID = seeker.BloodGroupID;
                userProfile.GenderID = seeker.GenderID;
            }
            else if (user.HospitalTables.Count > 0)
            {
                var hospital = user.HospitalTables.FirstOrDefault();
                userProfile.Hospital.HospitalID = hospital.HospitalID;
                userProfile.Hospital.FullName = hospital.FullName;
                userProfile.Hospital.Address = hospital.Address;
                userProfile.Hospital.PhoneNo = hospital.PhoneNo;
                userProfile.Hospital.WebSite = hospital.WebSite;
                userProfile.Hospital.Email = hospital.Email;
                userProfile.Hospital.Location = hospital.Location;
                userProfile.Hospital.CityID = hospital.CityID;
                userProfile.Hospital.UserID = hospital.UserID;

                userProfile.ContactNo = hospital.PhoneNo;
                userProfile.CityID = hospital.CityID;
            }
            else if (user.BloodBankTables.Count > 0)
            {
                var bloodBank = user.BloodBankTables.FirstOrDefault();
                userProfile.BloodBank.BloodBankID = bloodBank.BloodBankID;
                userProfile.BloodBank.BloodBankName = bloodBank.BloodBankName;
                userProfile.BloodBank.Address = bloodBank.Address;
                userProfile.BloodBank.PhoneNo = bloodBank.PhoneNo;
                userProfile.BloodBank.Location = bloodBank.Location;
                userProfile.BloodBank.WebSite = bloodBank.WebSite;
                userProfile.BloodBank.Email = bloodBank.Email;
                userProfile.BloodBank.CityID = bloodBank.CityID;
                userProfile.BloodBank.UserID = bloodBank.UserID;

                userProfile.ContactNo = bloodBank.PhoneNo;
                userProfile.CityID = bloodBank.CityID;
            }
            else if (user.DonorTables.Count > 0)
            {
                var donor = user.DonorTables.FirstOrDefault();
                userProfile.Donor.DonorID = donor.DonorID;
                userProfile.Donor.FullName = donor.FullName;
                userProfile.Donor.GenderID = donor.GenderID;
                userProfile.Donor.BloodGroupID = donor.BloodGroupID;
                userProfile.Donor.LastDonationDate = donor.LastDonationDate;
                userProfile.Donor.ContactNo = donor.ContactNo;
                userProfile.Donor.CNIC = donor.CNIC;
                userProfile.Donor.Location = donor.Location;
                userProfile.Donor.CityID = donor.CityID;
                userProfile.Donor.UserID = donor.UserID;

                userProfile.ContactNo = donor.ContactNo;
                userProfile.CityID = donor.CityID;
                userProfile.BloodGroupID = donor.BloodGroupID;
                userProfile.GenderID = donor.GenderID;
            }

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userProfile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", userProfile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userProfile.GenderID);
            
            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(RegistrationMV userProfile)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userProfile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", userProfile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userProfile.GenderID);

            return View(userProfile);
        }
    }
}