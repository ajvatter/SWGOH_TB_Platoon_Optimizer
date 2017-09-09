using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWGOH.Entities;
using SWGOH.Web.DataContexts;

namespace SWGOH.Web.Controllers
{
    public class PhaseTerritories1Controller : Controller
    {
        private SwgohDb db = new SwgohDb();

        // GET: PhaseTerritories1
        public ActionResult Index()
        {
            var phaseTerritories = db.PhaseTerritories.Include(p => p.TerritoryPlatoon1).Include(p => p.TerritoryPlatoon2).Include(p => p.TerritoryPlatoon3).Include(p => p.TerritoryPlatoon4).Include(p => p.TerritoryPlatoon5).Include(p => p.TerritoryPlatoon6);
            return View(phaseTerritories.ToList());
        }

        // GET: PhaseTerritories1/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseTerritory phaseTerritory = db.PhaseTerritories.Find(id);
            if (phaseTerritory == null)
            {
                return HttpNotFound();
            }
            return View(phaseTerritory);
        }

        // GET: PhaseTerritories1/Create
        public ActionResult Create()
        {
            ViewBag.TerritoryPlatoon1_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id");
            ViewBag.TerritoryPlatoon2_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id");
            ViewBag.TerritoryPlatoon3_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id");
            ViewBag.TerritoryPlatoon4_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id");
            ViewBag.TerritoryPlatoon5_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id");
            ViewBag.TerritoryPlatoon6_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id");
            return View();
        }

        // POST: PhaseTerritories1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TotalPointsEarned,TerritoryPlatoon1_Id,TerritoryPlatoon2_Id,TerritoryPlatoon3_Id,TerritoryPlatoon4_Id,TerritoryPlatoon5_Id,TerritoryPlatoon6_Id")] PhaseTerritory phaseTerritory)
        {
            if (ModelState.IsValid)
            {
                phaseTerritory.Id = Guid.NewGuid();
                db.PhaseTerritories.Add(phaseTerritory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TerritoryPlatoon1_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon1_Id);
            ViewBag.TerritoryPlatoon2_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon2_Id);
            ViewBag.TerritoryPlatoon3_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon3_Id);
            ViewBag.TerritoryPlatoon4_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon4_Id);
            ViewBag.TerritoryPlatoon5_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon5_Id);
            ViewBag.TerritoryPlatoon6_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon6_Id);
            return View(phaseTerritory);
        }

        // GET: PhaseTerritories1/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseTerritory phaseTerritory = db.PhaseTerritories.Find(id);
            if (phaseTerritory == null)
            {
                return HttpNotFound();
            }
            ViewBag.TerritoryPlatoon1_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon1_Id);
            ViewBag.TerritoryPlatoon2_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon2_Id);
            ViewBag.TerritoryPlatoon3_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon3_Id);
            ViewBag.TerritoryPlatoon4_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon4_Id);
            ViewBag.TerritoryPlatoon5_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon5_Id);
            ViewBag.TerritoryPlatoon6_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon6_Id);
            return View(phaseTerritory);
        }

        // POST: PhaseTerritories1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TotalPointsEarned,TerritoryPlatoon1_Id,TerritoryPlatoon2_Id,TerritoryPlatoon3_Id,TerritoryPlatoon4_Id,TerritoryPlatoon5_Id,TerritoryPlatoon6_Id")] PhaseTerritory phaseTerritory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phaseTerritory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TerritoryPlatoon1_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon1_Id);
            ViewBag.TerritoryPlatoon2_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon2_Id);
            ViewBag.TerritoryPlatoon3_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon3_Id);
            ViewBag.TerritoryPlatoon4_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon4_Id);
            ViewBag.TerritoryPlatoon5_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon5_Id);
            ViewBag.TerritoryPlatoon6_Id = new SelectList(db.TerritoryPlatoons, "Id", "Id", phaseTerritory.TerritoryPlatoon6_Id);
            return View(phaseTerritory);
        }

        // GET: PhaseTerritories1/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseTerritory phaseTerritory = db.PhaseTerritories.Find(id);
            if (phaseTerritory == null)
            {
                return HttpNotFound();
            }
            return View(phaseTerritory);
        }

        // POST: PhaseTerritories1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PhaseTerritory phaseTerritory = db.PhaseTerritories.Find(id);
            db.PhaseTerritories.Remove(phaseTerritory);
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
