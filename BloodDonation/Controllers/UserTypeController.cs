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
    public class UserTypeController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult AllUserType()
        {
            var userTypes = DB.UserTypeTables.ToList();
            var listUserTypes = new List<UserTypeMV>();

            foreach (var userType in userTypes)
            {
                var addUserType = new UserTypeMV();
                addUserType.UserTypeID = userType.UserTypeID;
                addUserType.UserType = userType.UserType;
                listUserTypes.Add(addUserType);
            }
            return View(listUserTypes);
        }

        public ActionResult Create()
        {
            var userType = new UserTypeMV();
            return View(userType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserTypeMV userTypeMV)
        {
            if (ModelState.IsValid)
            {
                var userTypeTable = new UserTypeTable();
                userTypeTable.UserTypeID = userTypeMV.UserTypeID;
                userTypeTable.UserType = userTypeMV.UserType;
                DB.UserTypeTables.Add(userTypeTable);
                DB.SaveChanges();
                return RedirectToAction("AllUserType");
            }

            return View(userTypeMV);
        }

        public ActionResult Edit(int? id)
        {
            var userType = DB.UserTypeTables.Find(id);
            if (userType == null)
            {
                return HttpNotFound();
            }
            var userTypeMV = new UserTypeMV();
            userTypeMV.UserTypeID = userType.UserTypeID;
            userTypeMV.UserType = userType.UserType;
            return View(userTypeMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTypeMV userTypeMV)
        {
            if (ModelState.IsValid)
            {
                var userTypeTable = new UserTypeTable();
                userTypeTable.UserTypeID = userTypeMV.UserTypeID;
                userTypeTable.UserType = userTypeMV.UserType;
                DB.Entry(userTypeTable).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("AllUserType");
            }
            return View(userTypeMV);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userType = DB.UserTypeTables.Find(id);
            if (userType == null)
            {
                return HttpNotFound();
            }
            var userTypeMV = new UserTypeMV();
            userTypeMV.UserTypeID = userType.UserTypeID;
            userTypeMV.UserType = userType.UserType;
            return View(userTypeMV);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            var userType = DB.UserTypeTables.Find(id);
            DB.UserTypeTables.Remove(userType);
            DB.SaveChanges();
            return RedirectToAction("AllUserType");
        }
    }
}