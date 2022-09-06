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
            var message = ViewData["Message"] == null ? "Welcome To Online Blood Donation Website" : ViewData["Message"];
            ViewData["Message"] = message;
            var registrationMV = new RegistrationMV();
            ViewBag.UserTypeID = new SelectList(DB.UserTypeTables.Where(ut => ut.UserTypeID > 1).ToList(), "UserTypeID", "UserType", "0");
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            return View(registrationMV);
        }

        public ActionResult Login()
        {
            var usermv = new UserMV();
            return View(usermv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserMV userMV)
        {
            if (ModelState.IsValid)
            {
                var user = DB.UserTables.Where(u => u.Password == userMV.Password && u.UserName == userMV.UserName).FirstOrDefault();
                if (user != null)
                {
                    if (user.AccountStatusID == 1)
                    {
                        ModelState.AddModelError(string.Empty, "Please Wait. Your Account is Under Review");
                    }
                    else if (user.AccountStatusID == 3)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is Rejected. For more details, Contact Us");
                    }
                    else if (user.AccountStatusID == 4)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is Suspended. For more details, Contact Us");
                    }
                    else if (user.AccountStatusID == 2)
                    {
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.UserName;
                        Session["Password"] = user.Password;
                        Session["EmailAddress"] = user.EmailAddress;
                        Session["AccountStatusID"] = user.AccountStatusID;
                        Session["AccountStatus"] = user.AccountStatusTable.AccountStatus;
                        Session["UserTypeID"] = user.UserTypeID;
                        Session["UserType"] = user.UserTypeTable.UserType;
                        Session["Description"] = user.Description;

                        if (user.UserTypeID == 1) // Admin
                        {
                            return RedirectToAction("AllNewUserRequests", "Accounts");
                        }
                        else if (user.UserTypeID == 2) // Donor
                        {
                            var donor = DB.DonorTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (donor != null)
                            {
                                Session["DonorID"] = donor.DonorID;
                                Session["FullName"] = donor.FullName;
                                Session["GenderID"] = donor.GenderID;
                                Session["BloodGroupID"] = donor.BloodGroupID;
                                Session["BloodGroup"] = donor.BloodGroupTable.BloodGroup;
                                Session["LastDonationDate"] = donor.LastDonationDate;
                                Session["ContactNo"] = donor.ContactNo;
                                Session["CNIC"] = donor.CNIC;
                                Session["Location"] = donor.Location;
                                Session["CityID"] = donor.CityID;
                                Session["City"] = donor.CityTable.City;

                                return RedirectToAction("Donor", "Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 3) // Seeker
                        {
                            var seeker = DB.SeekerTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (seeker != null)
                            {
                                Session["SeekerID"] = seeker.SeekerID;
                                Session["FullName"] = seeker.FullName;
                                Session["Age"] = seeker.Age;
                                Session["CityID"] = seeker.CityID;
                                Session["City"] = seeker.CityTable.City;
                                Session["BloodGroupID"] = seeker.BloodGroupID;
                                Session["BloodGroup"] = seeker.BloodGroupTable.BloodGroup;
                                Session["ContactNo"] = seeker.ContactNo;
                                Session["CNIC"] = seeker.CNIC;
                                Session["GenderID"] = seeker.GenderID;
                                Session["Gender"] = seeker.GenderTable.Gender;
                                Session["RegistrationDate"] = seeker.RegistrationDate;
                                Session["Address"] = seeker.Address;

                                return RedirectToAction("Seeker", "Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 4) // Hospital
                        {
                            var hospital = DB.HospitalTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (hospital != null)
                            {
                                Session["HospitalID"] = hospital.HospitalID;
                                Session["FullName"] = hospital.FullName;
                                Session["Address"] = hospital.Address;
                                Session["PhoneNo"] = hospital.PhoneNo;
                                Session["WebSite"] = hospital.WebSite;
                                Session["Email"] = hospital.Email;
                                Session["Location"] = hospital.Location;
                                Session["CityID"] = hospital.CityID;
                                Session["City"] = hospital.CityTable.City;

                                return RedirectToAction("Hospital", "Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        }
                        else if (user.UserTypeID == 5) // Blood Bank
                        {
                            var bloodBank = DB.BloodBankTables.Where(u => u.UserID == user.UserID).FirstOrDefault();
                            if (bloodBank != null)
                            {
                                Session["BloodBankID"] = bloodBank.BloodBankID;
                                Session["BloodBankName"] = bloodBank.BloodBankName;
                                Session["Address"] = bloodBank.Address;
                                Session["PhoneNo"] = bloodBank.PhoneNo;
                                Session["Location"] = bloodBank.Location;
                                Session["WebSite"] = bloodBank.WebSite;
                                Session["Email"] = bloodBank.Email;
                                Session["CityID"] = bloodBank.CityID;
                                Session["City"] = bloodBank.CityTable.City;

                                return RedirectToAction("BloodBank", "Dashboard");
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                            }
                        } 
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username & Password is Incorrect");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide Username and Password");
            }

            ClearSession();
            return View(userMV);
        }

        private void ClearSession()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["AccountStatusID"] = string.Empty;
            Session["AccountStatus"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["UserType"] = string.Empty;
            Session["Description"] = string.Empty;
        }

        public ActionResult Logout()
        {
            ClearSession();
            return RedirectToAction("MainHome");
        }
    }
}