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
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");

            return View(new FinderMV());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinderDonors(FinderMV finderMV)
        {
            int userID = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userID);

            var setDate = DateTime.Now.AddDays(-120);

            var donors = DB.DonorTables.Where(d => d.BloodGroupID == finderMV.BloodGroupID &&
                                                   d.CityID == finderMV.CityID &&
                                                   d.LastDonationDate < setDate).ToList();
            foreach (var donor in donors)
            {
                var user = DB.UserTables.Find(donor.UserID);

                if (userID != user.UserID)
                {
                    if (user.AccountStatusID == 2)  // Approved
                    {
                        var addDonor = new FinderSearchResultMV();
                        addDonor.BloodGroup = donor.BloodGroupTable.BloodGroup;
                        addDonor.BloodGroupID = donor.BloodGroupID;
                        addDonor.ContactNo = donor.ContactNo;
                        addDonor.UserID = donor.UserID;
                        addDonor.DonorID = donor.DonorID;
                        addDonor.FullName = donor.FullName;
                        addDonor.Address = donor.Location;
                        addDonor.UserType = "Person";
                        addDonor.UserTypeID = user.UserTypeID;
                        finderMV.SearchResult.Add(addDonor);
                    }
                }
            }

            var bloodBanks = DB.BloodBankStockTables.Where(d => d.BloodGroupID == finderMV.BloodGroupID &&
                                                                d.Quantity > 0).ToList();
            foreach (var bloodBank in bloodBanks)
            {
                var findBloodBank = DB.BloodBankTables.Find(bloodBank.BloodBankID);
                var user = DB.UserTables.Find(findBloodBank.UserID);

                if (userID != user.UserID)
                {
                    if (user.AccountStatusID == 2)  // Approved
                    {
                        var addDonor = new FinderSearchResultMV();
                        addDonor.BloodGroup = bloodBank.BloodGroupTable.BloodGroup;
                        addDonor.BloodGroupID = bloodBank.BloodGroupID;
                        addDonor.ContactNo = bloodBank.BloodBankTable.PhoneNo;
                        addDonor.DonorID = bloodBank.BloodBankID;
                        addDonor.UserID = bloodBank.BloodBankTable.UserID;
                        addDonor.FullName = bloodBank.BloodBankTable.BloodBankName;
                        addDonor.Address = bloodBank.BloodBankTable.Address;
                        addDonor.UserType = "Blood Bank";
                        addDonor.UserTypeID = user.UserTypeID;
                        finderMV.SearchResult.Add(addDonor);
                    }
                }
            }

            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", finderMV.BloodGroupID);
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", finderMV.CityID);

            return View(finderMV);
        }

        public ActionResult RequestForBlood(int? donorID, int? userTypeID, int? bloodGroupID)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return Redirect("/Home/MainHome#registrationSection");
            }

            var request = new RequestMV();
            request.AcceptedID = (int)donorID;

            if (userTypeID == 2)    // if Donor
            {
                request.AcceptedTypeID = 1;      // then Donor Request
            }
            else if (userTypeID == 5)   // if Blood Bank
            {
                request.AcceptedTypeID = 2;      // then Blood Bank Request
            }

            request.RequiredBloodGroupID = (int)bloodGroupID;

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestForBlood(RequestMV requestMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return Redirect("/Home/MainHome#registrationSection");
            }

            int userTypeID = 0;
            int requestTypeID = 0;
            int requestByID = 0;
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out userTypeID);

            if (userTypeID == 2) // Donor
            {
                int.TryParse(Convert.ToString(Session["DonorID"]), out requestByID);
            }
            else if (userTypeID == 3) // Seeker
            {
                requestTypeID = 1;
                int.TryParse(Convert.ToString(Session["SeekerID"]), out requestByID);
            }
            else if (userTypeID == 4) // Hospital
            {
                requestTypeID = 2;
                int.TryParse(Convert.ToString(Session["HospitalID"]), out requestByID);
            }
            else if (userTypeID == 5) // Blood Bank
            {
                requestTypeID = 3;
                int.TryParse(Convert.ToString(Session["BloodBankID"]), out requestByID);
            }

            if (ModelState.IsValid)
            {
                var request = new RequestTable();
                request.RequestDate = DateTime.Now;
                request.AcceptedTypeID = requestMV.AcceptedTypeID;
                request.AcceptedID = requestMV.AcceptedID;
                request.RequiredBloodGroupID = requestMV.RequiredBloodGroupID;
                request.ExpectedDate = requestMV.ExpectedDate;
                request.RequestDetails = requestMV.RequestDetails;
                request.RequestStatusID = 1;
                request.RequestByID = requestByID;
                request.RequestTypeID = requestTypeID;
                DB.RequestTables.Add(request);
                DB.SaveChanges();
                return RedirectToAction("ShowAllRequests");
            }

            return View(requestMV);
        }

        public ActionResult ShowAllRequests()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userTypeID = 0;
            int requestTypeID = 0;
            int requestByID = 0;
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out userTypeID);

            if (userTypeID == 2) // Donor
            {
                int.TryParse(Convert.ToString(Session["DonorID"]), out requestByID);
            }
            else if (userTypeID == 3) // Seeker
            {
                requestTypeID = 1;
                int.TryParse(Convert.ToString(Session["SeekerID"]), out requestByID);
            }
            else if (userTypeID == 4) // Hospital
            {
                requestTypeID = 2;
                int.TryParse(Convert.ToString(Session["HospitalID"]), out requestByID);
            }
            else if (userTypeID == 5) // Blood Bank
            {
                requestTypeID = 3;
                int.TryParse(Convert.ToString(Session["BloodBankID"]), out requestByID);
            }

            var requests = DB.RequestTables.Where(r => r.RequestByID == requestByID &&
                                                       r.RequestTypeID == requestTypeID).ToList();

            var requestList = new List<RequestListMV>();
            foreach (var request in requestList)
            {
                var addRequest = new RequestListMV();

                addRequest.RequestID = request.RequestID;
                addRequest.RequestDate = request.RequestDate;
                addRequest.RequestByID = request.RequestByID;
                addRequest.AcceptedID = request.AcceptedID;
                addRequest.AcceptedTypeID = request.AcceptedTypeID;
                addRequest.RequiredBloodGroupID = request.RequiredBloodGroupID;
                addRequest.RequestTypeID = request.RequestTypeID;
                addRequest.RequestStatusID = request.RequestStatusID;
                addRequest.ExpectedDate = request.ExpectedDate;
                addRequest.RequestDetails = request.RequestDetails;

                if (request.AcceptedTypeID == 1)    // Donor
                {
                    var getDonor = DB.DonorTables.Find(request.AcceptedID);
                    addRequest.AcceptedFullName = getDonor.FullName;
                    addRequest.ContactNo = getDonor.ContactNo;
                    addRequest.Address = getDonor.Location;
                }
                else if (request.AcceptedTypeID == 2)   // Blood Bank
                {
                    var getBloodBank = DB.BloodBankTables.Find(request.AcceptedID);
                    addRequest.AcceptedFullName = getBloodBank.BloodBankName;
                    addRequest.ContactNo = getBloodBank.PhoneNo;
                    addRequest.Address = getBloodBank.Address;
                }

                requestList.Add(addRequest);
            }

            return View(requestList);
        }
    }
}