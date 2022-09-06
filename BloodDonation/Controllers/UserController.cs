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

            if (ModelState.IsValid)
            {
                var checkEmailAddress = DB.UserTables.Where(u => u.EmailAddress == userProfile.User.EmailAddress &&
                                                                 u.UserID != userProfile.User.UserID).FirstOrDefault();
                if (checkEmailAddress == null)
                {
                    try
                    {
                        var user = DB.UserTables.Find(userProfile.User.UserID);
                        user.EmailAddress = userProfile.User.EmailAddress;
                        DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();

                        if (userProfile.Donor.DonorID > 0)
                        {
                            var donor = DB.DonorTables.Find(userProfile.Donor.DonorID);
                            donor.FullName = userProfile.Donor.FullName;
                            donor.BloodGroupID = userProfile.BloodGroupID;
                            donor.GenderID = userProfile.GenderID;
                            donor.ContactNo = userProfile.Donor.ContactNo;
                            donor.CNIC = userProfile.Donor.CNIC;
                            donor.CityID = userProfile.CityID;
                            donor.Location = userProfile.Donor.Location;
                            DB.Entry(donor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userProfile.Seeker.SeekerID > 0)
                        {
                            var seeker = DB.SeekerTables.Find(userProfile.Seeker.SeekerID);
                            seeker.FullName = userProfile.Seeker.FullName;
                            seeker.BloodGroupID = userProfile.BloodGroupID;
                            seeker.GenderID = userProfile.GenderID;
                            seeker.Age = userProfile.Seeker.Age;
                            seeker.ContactNo = userProfile.Seeker.ContactNo;
                            seeker.CNIC = userProfile.Seeker.CNIC;
                            seeker.CityID = userProfile.Seeker.CityID;
                            seeker.Address = userProfile.Seeker.Address;
                            DB.Entry(seeker).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userProfile.BloodBank.BloodBankID > 0)
                        {
                            var bloodBank = DB.BloodBankTables.Find(userProfile.BloodBank.BloodBankID);
                            bloodBank.BloodBankName = userProfile.BloodBank.BloodBankName;
                            bloodBank.PhoneNo = userProfile.BloodBank.PhoneNo;
                            bloodBank.Email = userProfile.BloodBank.Email;
                            bloodBank.WebSite = userProfile.BloodBank.WebSite;
                            bloodBank.CityID = userProfile.CityID;
                            bloodBank.Address = userProfile.BloodBank.Address;
                            DB.Entry(bloodBank).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userProfile.Hospital.HospitalID > 0)
                        {
                            var hospital = DB.HospitalTables.Find(userProfile.Hospital.HospitalID);
                            hospital.FullName = userProfile.Hospital.FullName;
                            hospital.PhoneNo = userProfile.Hospital.PhoneNo;
                            hospital.Email = userProfile.Hospital.Email;
                            hospital.WebSite = userProfile.Hospital.WebSite;
                            hospital.CityID = userProfile.CityID;
                            hospital.Address = userProfile.Hospital.Address;
                            DB.Entry(hospital).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }

                        return RedirectToAction("UserProfile", "User", new { id = userProfile.User.UserID });
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "User Email is Already Exist");
                    }
                } 
                else
                {
                    ModelState.AddModelError(string.Empty, "User Email is Already Exist");
                    return View(userProfile);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Some Data is Incorrect. Please Provide Correct Details");
            }

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userProfile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", userProfile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", userProfile.GenderID);

            return View(userProfile);
        }
    }
}