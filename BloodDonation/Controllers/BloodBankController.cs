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

            var bloodBankStockList = new List<BloodBankStockMV>();
            int bloodBankID = 0;
            int.TryParse(Convert.ToString(Session["BloodBankID"]), out bloodBankID);
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
    }
}