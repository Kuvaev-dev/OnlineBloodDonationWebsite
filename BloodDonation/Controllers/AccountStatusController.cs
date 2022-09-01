using BloodDonation.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class AccountStatusController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult AllAccountStatus()
        {
            var accountStatuses = DB.AccountStatusTables.ToList();
            var listAccountStatuses = new List<AccountStatusMV>();

            foreach (var accountStatus in accountStatuses)
            {
                var addAccountStatus = new AccountStatusMV();
                addAccountStatus.AccountStatusID = accountStatus.AccountStatusID;
                addAccountStatus.AccountStatus = accountStatus.AccountStatus;
                listAccountStatuses.Add(addAccountStatus);
            }
            return View(listAccountStatuses);
        }

        public ActionResult Create()
        {
            var accountStatus = new AccountStatusMV();
            return View(accountStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountStatusMV accountStatusMV)
        {
            if (ModelState.IsValid)
            {
                var checkAccountStatus = DB.AccountStatusTables.Where(bg => bg.AccountStatus == accountStatusMV.AccountStatus).FirstOrDefault();
                if (checkAccountStatus == null)
                {
                    var accountStatusTable = new AccountStatusTable();
                    accountStatusTable.AccountStatusID = accountStatusMV.AccountStatusID;
                    accountStatusTable.AccountStatus = accountStatusMV.AccountStatus;
                    DB.AccountStatusTables.Add(accountStatusTable);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("AccountStatus", "Already exist");
                }
            }
            return View(accountStatusMV);
        }

        public ActionResult Edit(int? id)
        {
            var accountStatus = DB.AccountStatusTables.Find(id);
            if (accountStatus == null)
            {
                return HttpNotFound();
            }
            var accountStatusMV = new AccountStatusMV();
            accountStatusMV.AccountStatusID = accountStatus.AccountStatusID;
            accountStatusMV.AccountStatus = accountStatus.AccountStatus;
            return View(accountStatusMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountStatusMV accountStatusMV)
        {
            if (ModelState.IsValid)
            {
                var checkAccountStatus = DB.AccountStatusTables.Where(accs => accs.AccountStatus == accountStatusMV.AccountStatus &&
                                                                        accs.AccountStatusID != accountStatusMV.AccountStatusID).FirstOrDefault();
                if (checkAccountStatus == null)
                {
                    var accountStatusTable = new AccountStatusTable();
                    accountStatusTable.AccountStatusID = accountStatusMV.AccountStatusID;
                    accountStatusTable.AccountStatus = accountStatusMV.AccountStatus;
                    DB.Entry(accountStatusTable).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("AccountStatus", "Already exist");
                }
            }
            return View(accountStatusMV);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountStatus = DB.AccountStatusTables.Find(id);
            if (accountStatus == null)
            {
                return HttpNotFound();
            }
            var accountStatusMV = new AccountStatusMV();
            accountStatusMV.AccountStatusID = accountStatus.AccountStatusID;
            accountStatusMV.AccountStatus = accountStatus.AccountStatus;
            return View(accountStatusMV);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            var accountStatus = DB.AccountStatusTables.Find(id);
            DB.AccountStatusTables.Remove(accountStatus);
            DB.SaveChanges();
            return RedirectToAction("AllAccountStatus");
        }
    }
}