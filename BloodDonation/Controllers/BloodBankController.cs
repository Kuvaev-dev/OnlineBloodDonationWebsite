using BloodDonation.Helpers;
using BloodDonation.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class BloodBankController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult BloodBankStock()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int bloodBankID = 0;
            string getBloodBankID = Convert.ToString(Session["BloodBankID"]);
            int.TryParse(getBloodBankID, out bloodBankID);

            var bloodBankStockList = new List<BloodBankStockMV>();
            var stockList = DB.BloodBankStockTables.Where(b => b.BloodBankID == bloodBankID);

            foreach (var stock in stockList)
            {
                var bloodBankStockMV = new BloodBankStockMV();
                bloodBankStockMV.BloodBankStockID = stock.BloodBankStockID;
                bloodBankStockMV.BloodBankID = stock.BloodBankID;
                bloodBankStockMV.BloodBank = stock.BloodBankTable.BloodBankName;
                bloodBankStockMV.BloodGroupID = stock.BloodGroupID;
                bloodBankStockMV.BloodGroup = stock.BloodGroupTable.BloodGroup;
                bloodBankStockMV.Quantity = stock.Quantity;
                bloodBankStockMV.Status = stock.Status == true ? "Ready For Use" : "Not Ready";
                bloodBankStockMV.Description = stock.Description;
                bloodBankStockList.Add(bloodBankStockMV);
            }

            return View(bloodBankStockList);
        }

        public ActionResult AllCampaigns()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int bloodBankID = 0;
            int.TryParse(Convert.ToString(Session["BloodBankID"]), out bloodBankID);
            var allCampaigns = DB.CampaignTables.Where(c => c.BloodBankID == bloodBankID);

            if (allCampaigns.Count() > 0)
            {
                allCampaigns = allCampaigns.OrderByDescending(o => o.CampaignID);
            }

            return View(allCampaigns);
        }

        public ActionResult NewCampaign()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var campaignMV = new CampaignMV();

            return View(campaignMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCampaign(CampaignMV campaignMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int bloodBankId = 0;
            int.TryParse(Convert.ToString(Session["BloodBankID"]), out bloodBankId);

            campaignMV.BloodBankID = bloodBankId;
            
            if (ModelState.IsValid)
            {
                var campaign = new CampaignTable();
                campaign.BloodBankID = bloodBankId;
                campaign.CampaignDate = campaignMV.CampaignDate;
                campaign.StartTime = campaignMV.StartTime;
                campaign.EndTime = campaignMV.EndTime;
                campaign.Location = campaignMV.Location;
                campaign.CampaignDetails = campaignMV.CampaignDetails;
                campaign.CampaignTitle = campaignMV.CampaignTitle;
                campaign.CampaignPhoto = "~/Content/CampaignPhoto/testlogo.jpg";
                DB.CampaignTables.Add(campaign);
                DB.SaveChanges();

                if (campaignMV.CampaignPhotoFile != null)
                {
                    var folder = "~/Content/CampaignPhoto";
                    var file = string.Format("{0}.jpg", campaignMV.CampaignID);
                    var response = FileHelper.UploadPhoto(campaignMV.CampaignPhotoFile, folder, file);

                    if (response)
                    {
                        var picture = string.Format("{0}/{1}", folder, file);
                        campaign.CampaignPhoto = picture;
                        DB.Entry(campaign).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                    }
                }

                return RedirectToAction("AllCampaigns");
            }

            return View(campaignMV);
        }
    }
}