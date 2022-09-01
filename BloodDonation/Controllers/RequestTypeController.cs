using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using BloodDonation.Models;

namespace BloodDonation.Controllers
{
    public class RequestTypeController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult AllRequestType()
        {
            var requestTypes = DB.RequestTypeTables.ToList();
            var listRequestTypes = new List<RequestTypeMV>();

            foreach (var requestType in requestTypes)
            {
                var addRequestType = new RequestTypeMV();
                addRequestType.RequestTypeID = requestType.RequestTypeID;
                addRequestType.RequestType = requestType.RequestType;
                listRequestTypes.Add(addRequestType);
            }
            return View(listRequestTypes);
        }

        public ActionResult Create()
        {
            var requestType = new RequestTypeMV();
            return View(requestType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestTypeMV requestTypeMV)
        {
            if (ModelState.IsValid)
            {
                var requestTypeTable = new RequestTypeTable();
                requestTypeTable.RequestTypeID = requestTypeMV.RequestTypeID;
                requestTypeTable.RequestType = requestTypeMV.RequestType;
                DB.RequestTypeTables.Add(requestTypeTable);
                DB.SaveChanges();
                return RedirectToAction("AllRequestType");
            }
               
            return View(requestTypeMV);
        }

        public ActionResult Edit(int? id)
        {
            var requestType = DB.RequestTypeTables.Find(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            var requestTypeMV = new RequestTypeMV();
            requestTypeMV.RequestTypeID = requestType.RequestTypeID;
            requestTypeMV.RequestType = requestType.RequestType;
            return View(requestTypeMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequestTypeMV requestTypeMV)
        {
            if (ModelState.IsValid)
            {
                var requestTypeTable = new RequestTypeTable();
                requestTypeTable.RequestTypeID = requestTypeMV.RequestTypeID;
                requestTypeTable.RequestType = requestTypeMV.RequestType;
                DB.Entry(requestTypeTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllRequestType");
            }
            return View(requestTypeMV);
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
            var requestTypeMV = new RequestTypeMV();
            requestTypeMV.RequestTypeID = requestType.RequestTypeID;
            requestTypeMV.RequestType = requestType.RequestType;
            return View(requestTypeMV);
        }

        [HttpPost, ActionName("Delete")]
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