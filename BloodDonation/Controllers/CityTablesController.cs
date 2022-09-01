using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;

namespace BloodDonation.Controllers
{
    public class CityTablesController : Controller
    {
        private db_a8c2c8_blooddonationEntities db = new db_a8c2c8_blooddonationEntities();

        // GET: CityTables
        public ActionResult Index()
        {
            return View(db.CityTables.ToList());
        }

        // GET: CityTables/Create
        public ActionResult Create()
        {
            var city = new CityTable();
            return View(city);
        }

        // POST: CityTables/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityID,City")] CityTable cityTable)
        {
            if (ModelState.IsValid)
            {
                db.CityTables.Add(cityTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cityTable);
        }

        // GET: CityTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityTable cityTable = db.CityTables.Find(id);
            if (cityTable == null)
            {
                return HttpNotFound();
            }
            return View(cityTable);
        }

        // POST: CityTables/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityID,City")] CityTable cityTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cityTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cityTable);
        }

        // GET: CityTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityTable cityTable = db.CityTables.Find(id);
            if (cityTable == null)
            {
                return HttpNotFound();
            }
            return View(cityTable);
        }

        // POST: CityTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CityTable cityTable = db.CityTables.Find(id);
            db.CityTables.Remove(cityTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
