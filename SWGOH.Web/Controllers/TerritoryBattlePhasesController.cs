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
    public class TerritoryBattlePhasesController : Controller
    {
        private SwgohDb db = new SwgohDb();

        // GET: TerritoryBattlePhases
        public ActionResult Index()
        {
            var territoryBattlePhases = db.TerritoryBattlePhases.Include(t => t.Territory1).Include(t => t.Territory2);
            return View(territoryBattlePhases.ToList());
        }

        // GET: TerritoryBattlePhases/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryBattlePhase territoryBattlePhase = db.TerritoryBattlePhases.Find(id);
            if (territoryBattlePhase == null)
            {
                return HttpNotFound();
            }
            return View(territoryBattlePhase);
        }

        // GET: TerritoryBattlePhases/Create
        public ActionResult Create()
        {
            ViewBag.Territory1_Id = new SelectList(db.PhaseTerritories, "Id", "Id");
            ViewBag.Territory2_Id = new SelectList(db.PhaseTerritories, "Id", "Id");
            return View();
        }

        // POST: TerritoryBattlePhases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequiredStars,Territory1_Id,HasSecondTerritory,Territory2_Id")] TerritoryBattlePhase territoryBattlePhase)
        {
            if (ModelState.IsValid)
            {
                territoryBattlePhase.Id = Guid.NewGuid();
                db.TerritoryBattlePhases.Add(territoryBattlePhase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Territory1_Id = new SelectList(db.PhaseTerritories, "Id", "Id", territoryBattlePhase.Territory1_Id);
            ViewBag.Territory2_Id = new SelectList(db.PhaseTerritories, "Id", "Id", territoryBattlePhase.Territory2_Id);
            return View(territoryBattlePhase);
        }

        // GET: TerritoryBattlePhases/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryBattlePhase territoryBattlePhase = db.TerritoryBattlePhases.Find(id);
            if (territoryBattlePhase == null)
            {
                return HttpNotFound();
            }
            ViewBag.Territory1_Id = new SelectList(db.PhaseTerritories, "Id", "Id", territoryBattlePhase.Territory1_Id);
            ViewBag.Territory2_Id = new SelectList(db.PhaseTerritories, "Id", "Id", territoryBattlePhase.Territory2_Id);
            return View(territoryBattlePhase);
        }

        // POST: TerritoryBattlePhases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequiredStars,Territory1_Id,HasSecondTerritory,Territory2_Id")] TerritoryBattlePhase territoryBattlePhase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territoryBattlePhase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Territory1_Id = new SelectList(db.PhaseTerritories, "Id", "Id", territoryBattlePhase.Territory1_Id);
            ViewBag.Territory2_Id = new SelectList(db.PhaseTerritories, "Id", "Id", territoryBattlePhase.Territory2_Id);
            return View(territoryBattlePhase);
        }

        // GET: TerritoryBattlePhases/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryBattlePhase territoryBattlePhase = db.TerritoryBattlePhases.Find(id);
            if (territoryBattlePhase == null)
            {
                return HttpNotFound();
            }
            return View(territoryBattlePhase);
        }

        // POST: TerritoryBattlePhases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TerritoryBattlePhase territoryBattlePhase = db.TerritoryBattlePhases.Find(id);
            db.TerritoryBattlePhases.Remove(territoryBattlePhase);
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
