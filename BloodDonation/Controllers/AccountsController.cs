using DatabaseLayer;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace BloodDonation.Controllers
{
    public class AccountsController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult AllNewUserRequests()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var users = DB.UserTables.Where(u => u.AccountStatusID == 1).ToList();
            return View(users);
        }

        public ActionResult UserDetails(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var user = DB.UserTables.Find(id);
            return View(user);
        }

        public ActionResult UserApproved(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 2;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequests");
        }

        public ActionResult UserRejected(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 3;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("AllNewUserRequests");
        }

        public ActionResult AddNewDonorByBloodBank()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var collectBloodMV = new CollectBloodMV();

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", "0");

            return View(collectBloodMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDonorByBloodBank(CollectBloodMV collectBloodMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int bloodBankID = 0;
            string getBloodBankID = Convert.ToString(Session["BloodBankID"]);
            int.TryParse(getBloodBankID, out bloodBankID);
            var currentDate = DateTime.Now.Date;
            var currentCampaign = DB.CampaignTables.Where(c => c.CampaignDate == currentDate &&
                                                               c.BloodBankID == bloodBankID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        var checkDonor = DB.DonorTables.Where(d => d.CNIC.Trim().Replace("-", "") == collectBloodMV.DonorDetails.CNIC.Trim().Replace("-", "")).FirstOrDefault();
                        if (checkDonor == null)
                        {
                            var user = new UserTable();
                            user.UserName = collectBloodMV.DonorDetails.FullName.Trim();
                            user.Password = "12345";    // Default
                            user.EmailAddress = collectBloodMV.DonorDetails.EmailAddress;
                            user.AccountStatusID = 2;   // Approved
                            user.UserTypeID = 2;    // Donor User
                            user.Description = "Add By Blood Bank";
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var donor = new DonorTable();
                            donor.FullName = collectBloodMV.DonorDetails.FullName;
                            donor.BloodGroupID = collectBloodMV.BloodGroupID;
                            donor.Location = collectBloodMV.DonorDetails.Location;
                            donor.ContactNo = collectBloodMV.DonorDetails.ContactNo;
                            donor.LastDonationDate = DateTime.Now;
                            donor.CNIC = collectBloodMV.DonorDetails.CNIC;
                            donor.GenderID = collectBloodMV.GenderID;
                            donor.CityID = collectBloodMV.CityID;
                            donor.UserID = user.UserID;
                            DB.DonorTables.Add(donor);

                            DB.SaveChanges();

                            checkDonor = DB.DonorTables.Where(d => d.CNIC.Trim().Replace("-", "") == collectBloodMV.DonorDetails.CNIC.Trim().Replace("-", "")).FirstOrDefault();
                        }

                        var checkBloodGroupStock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodBankID &&
                                                                                      s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();
                        if (checkBloodGroupStock == null)
                        {
                            var bloodBankStock = new BloodBankStockTable();
                            bloodBankStock.BloodBankID = bloodBankID;
                            bloodBankStock.BloodGroupID = collectBloodMV.BloodGroupID;
                            bloodBankStock.Quantity = 0;
                            bloodBankStock.Status = true;
                            bloodBankStock.Description = "";
                            DB.BloodBankStockTables.Add(bloodBankStock);

                            DB.SaveChanges();

                            checkBloodGroupStock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodBankID &&
                                                                                      s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();
                        }

                        checkBloodGroupStock.Quantity += collectBloodMV.Quantity;
                        DB.Entry(checkBloodGroupStock).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();

                        var collectBloodDetail = new BloodBankStockDetailTable();
                        collectBloodDetail.BloodBankStockID = checkBloodGroupStock.BloodBankStockID;
                        collectBloodDetail.BloodGroupID = collectBloodMV.BloodGroupID;
                        collectBloodDetail.CampaignID = currentCampaign.CampaignID;
                        collectBloodDetail.Quantity = collectBloodMV.Quantity;
                        collectBloodDetail.DonorID = checkDonor.DonorID;
                        collectBloodDetail.DonateDateTime = DateTime.Now;
                        DB.BloodBankStockDetailTables.Add(collectBloodDetail);
                        DB.SaveChanges();

                        transaction.Commit();
                        return RedirectToAction("BloodBankStock", "BloodBank");
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
                ModelState.AddModelError(string.Empty, "Please Provide Full Donor Details");
            }

            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", collectBloodMV.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", collectBloodMV.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.GenderTables.ToList(), "GenderID", "Gender", collectBloodMV.GenderID);

            return View(collectBloodMV);
        }
    }
}