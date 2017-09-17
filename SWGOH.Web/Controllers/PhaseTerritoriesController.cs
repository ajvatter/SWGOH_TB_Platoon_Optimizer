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
using SWGOH.Web.ViewModels;
using AutoMapper;

namespace SWGOH.Web.Controllers
{
    public class PhaseTerritoriesController : Controller
    {
        private SwgohDb db = new SwgohDb();

        // GET: PhaseTerritories
        public ActionResult Index()
        {
            var phaseTerritories = db.PhaseTerritories;
            return View(phaseTerritories.ToList());
        }

        // GET: PhaseTerritories/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseTerritory phaseTerritory = db.PhaseTerritories.Include(x => x.TerritoryBattlePhase).SingleOrDefault(x => x.Id == id);
            if (phaseTerritory == null)
            {
                return HttpNotFound();
            }
            PhaseTerritoryModel model = Mapper.Map<PhaseTerritory, PhaseTerritoryModel>(phaseTerritory);
            return View(model);
        }

        // GET: PhaseTerritories/Create
        [Authorize]
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

        // POST: PhaseTerritories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhaseTerritory phaseTerritory)
        {
            if (ModelState.IsValid)
            {
                phaseTerritory.Id = Guid.NewGuid();
                db.PhaseTerritories.Add(phaseTerritory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phaseTerritory);
        }

        // GET: PhaseTerritories/Edit/5
        [Authorize]
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

            return View(phaseTerritory);
        }

        // POST: PhaseTerritories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TotalPointsEarned,TerritoryPlatoon1_Id,TerritoryPlatoon2_Id,TerritoryPlatoon3_Id,TerritoryPlatoon4_Id,TerritoryPlatoon5_Id,TerritoryPlatoon6_Id")] PhaseTerritory phaseTerritory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phaseTerritory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phaseTerritory);
        }

        // GET: PhaseTerritories/Delete/5
        [Authorize]
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

        // POST: PhaseTerritories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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
