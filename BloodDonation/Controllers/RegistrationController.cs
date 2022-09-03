using BloodDonation.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class RegistrationController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();
        static ReigstrationMV registrationmv;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectUser(ReigstrationMV reigstrationMV)
        {
            registrationmv = reigstrationMV;

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
        }

        public ActionResult HospitalUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HospitalUser(ReigstrationMV reigstrationMV)
        {
            if (ModelState.IsValid)
            {
                var checkTitle = DB.HospitalTables.Where(h => h.FullName == reigstrationMV.Hospital.FullName.Trim()).FirstOrDefault();
                if (checkTitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable();
                            user.UserName = reigstrationMV.User.UserName;
                            user.Password = reigstrationMV.User.Password;
                            user.EmailAddress = reigstrationMV.User.EmailAddress;
                            user.AccountStatusID = 1;
                            user.UserTypeID = reigstrationMV.UserTypeID;
                            user.Description = reigstrationMV.User.Description;
                            DB.UserTables.Add(user);

                            var hospital = new HospitalTable();
                            hospital.FullName = reigstrationMV.Hospital.FullName;
                            hospital.Address = reigstrationMV.Hospital.Address;
                            hospital.PhoneNo = reigstrationMV.Hospital.PhoneNo;
                            hospital.WebSite = reigstrationMV.Hospital.WebSite;
                            hospital.Email = reigstrationMV.Hospital.Email;
                            hospital.Location = reigstrationMV.Hospital.Address;
                            hospital.CityID = reigstrationMV.CityID;
                            hospital.UserID = user.UserID;
                            DB.HospitalTables.Add(hospital);

                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks for registration! Your query will be review shortly!";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch 
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Details");
                            transaction.Rollback();
                        }
                    }
                } 
                else
                {
                    ModelState.AddModelError(string.Empty, "Hospital Already Registered");
                }
            }

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", reigstrationMV.CityID);
            return View(reigstrationMV);
        }

        public ActionResult DonorUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", registrationmv.BloodGroupID);
            return View(registrationmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorUser(ReigstrationMV reigstrationMV)
        {
            if (ModelState.IsValid)
            {
                var checkTitle = DB.DonorTables.Where(h => h.FullName == reigstrationMV.Donor.FullName.Trim() &&
                                                            h.CNIC == reigstrationMV.Donor.CNIC).FirstOrDefault();
                if (checkTitle == null)
                {
                    using (var transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = new UserTable();
                            user.UserName = reigstrationMV.User.UserName;
                            user.Password = reigstrationMV.User.Password;
                            user.EmailAddress = reigstrationMV.User.EmailAddress;
                            user.AccountStatusID = 1;
                            user.UserTypeID = reigstrationMV.UserTypeID;
                            user.Description = reigstrationMV.User.Description;
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var donor = new DonorTable();
                            donor.FullName = reigstrationMV.Donor.FullName;
                            donor.BloodGroupID = reigstrationMV.BloodGroupID;
                            donor.Location = reigstrationMV.Donor.Location;
                            donor.ContactNo = reigstrationMV.Donor.ContactNo;
                            donor.LastDonationDate = reigstrationMV.Donor.LastDonationDate;
                            donor.CNIC = reigstrationMV.Donor.CNIC;
                            donor.CityID = reigstrationMV.CityID;
                            donor.UserID = user.UserID;
                            DB.DonorTables.Add(donor);

                            DB.SaveChanges();
                            transaction.Commit();
                            ViewData["Message"] = "Thanks for registration! Your query will be review shortly!";
                            return RedirectToAction("MainHome", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelError(string.Empty, "Please Provide Correct Details");
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Donor Already Registered");
                }
            }

            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", reigstrationMV.BloodGroupID);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", reigstrationMV.CityID);
            return View(reigstrationMV);
        }

        public ActionResult BloodBankUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodBankUser(ReigstrationMV reigstrationMV)
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View();
        }

        public ActionResult SeekerUser()
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View(registrationmv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeekerUser(ReigstrationMV reigstrationMV)
        {
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", registrationmv.CityID);
            return View();
        }
    }
}