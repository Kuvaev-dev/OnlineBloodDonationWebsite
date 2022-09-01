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
    public class BloodGroupController : Controller
    {
        db_a8c2c8_blooddonationEntities DB = new db_a8c2c8_blooddonationEntities();

        public ActionResult AllBloodGroups()
        {
            var bloodGroups = DB.BloodGroupTables.ToList();
            var listBloodGroups = new List<BloodGroupsMV>();

            foreach (var bloodGroup in bloodGroups)
            {
                var addBloodGroup = new BloodGroupsMV();
                addBloodGroup.BloodGroupID = bloodGroup.BloodGroupID;
                addBloodGroup.BloodGroup = bloodGroup.BloodGroup;
                listBloodGroups.Add(addBloodGroup);
            }
            return View(listBloodGroups);
        }

        public ActionResult Create()
        {
            var bloodGroup = new BloodGroupsMV();
            return View(bloodGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloodGroupsMV bloodGroupsMV)
        {
            var checkBloodGroup = DB.BloodGroupTables.Where(bg => bg.BloodGroup == bloodGroupsMV.BloodGroup).FirstOrDefault();
            if (checkBloodGroup == null)
            {
                var bloodGroupTable = new BloodGroupTable();
                bloodGroupTable.BloodGroupID = bloodGroupsMV.BloodGroupID;
                bloodGroupTable.BloodGroup = bloodGroupsMV.BloodGroup;
                DB.BloodGroupTables.Add(bloodGroupTable);
                DB.SaveChanges();
                return RedirectToAction("AllBloodGroups");
            } 
            else
            {
                ModelState.AddModelError("BloodGroup", "Already exist");
            }

            return View(bloodGroupsMV);
        }

        public ActionResult Edit(int? id)
        {
            var bloodGroup = DB.BloodGroupTables.Find(id);
            if (bloodGroup == null)
            {
                return HttpNotFound();
            }
            var bloodGroupsMV = new BloodGroupsMV();
            bloodGroupsMV.BloodGroupID = bloodGroup.BloodGroupID;
            bloodGroupsMV.BloodGroup = bloodGroup.BloodGroup;
            return View(bloodGroupsMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BloodGroupsMV bloodGroupsMV)
        {
            if (ModelState.IsValid)
            {
                var checkBloodGroup = DB.BloodGroupTables.Where(bg => bg.BloodGroup == bloodGroupsMV.BloodGroup &&
                                                                        bg.BloodGroupID != bloodGroupsMV.BloodGroupID).FirstOrDefault();
                if (checkBloodGroup == null)
                {
                    var bloodGroupTable = new BloodGroupTable();
                    bloodGroupTable.BloodGroupID = bloodGroupsMV.BloodGroupID;
                    bloodGroupTable.BloodGroup = bloodGroupsMV.BloodGroup;
                    DB.Entry(bloodGroupTable).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBloodGroups");
                }
                else
                {
                    ModelState.AddModelError("BloodGroup", "Already exist");
                }
            }
            return View(bloodGroupsMV);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bloodGroup = DB.BloodGroupTables.Find(id);
            if (bloodGroup == null)
            {
                return HttpNotFound();
            }
            var bloodGroupMV = new BloodGroupsMV();
            bloodGroupMV.BloodGroupID = bloodGroup.BloodGroupID;
            bloodGroupMV.BloodGroup = bloodGroup.BloodGroup;
            return View(bloodGroupMV);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            var bloodGroup = DB.BloodGroupTables.Find(id);
            DB.BloodGroupTables.Remove(bloodGroup);
            DB.SaveChanges();
            return RedirectToAction("AllBloodGroups");
        }
    }
}