using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace BloodDonation.Controllers
{
    public class RequestTypeController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult AllRequestType()
        {
            var requestTypes = DB.RequestTypeTables.ToList();
            return View(requestTypes);
        }

        public ActionResult Create()
        {
            var requestType = new RequestTypeTable();
            return View(requestType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestTypeTable requestTypeTable)
        {
            if (ModelState.IsValid)
            {
                DB.RequestTypeTables.Add(requestTypeTable);
                DB.SaveChanges();
                return RedirectToAction("AllRequestType");
            }
               
            return View(requestTypeTable);
        }

        public ActionResult Edit(int? id)
        {
            var requestType = DB.RequestTypeTables.Find(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequestTypeTable requestTypeTable)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(requestTypeTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllRequestType");
            }
            return View(requestTypeTable);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var requestType = DB.RequestTypeTables.Find(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            var requestType = DB.RequestTypeTables.Find(id);
            DB.RequestTypeTables.Remove(requestType);
            DB.SaveChanges();
            return RedirectToAction("AllRequestType");
        }
    }
}