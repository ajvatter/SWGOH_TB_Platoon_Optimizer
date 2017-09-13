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
    public class TerritoryPlatoonsController : Controller
    {
        private SwgohDb db = new SwgohDb();

        // GET: TerritoryPlatoons
        public ActionResult Index()
        {
            var territoryPlatoons = db.TerritoryPlatoons.ToList();
            return View(territoryPlatoons);
        }

        // GET: TerritoryPlatoons/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }
            TerritoryPlatoonModel model = Mapper.Map<TerritoryPlatoon, TerritoryPlatoonModel>(territoryPlatoon);
            return View(model);
        }

        // GET: TerritoryPlatoons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TerritoryPlatoons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Character1_Id,Character2_Id,Character3_Id,Character4_Id,Character5_Id,Character6_Id,Character7_Id,Character8_Id,Character9_Id,Character10_Id,Character11_Id,Character12_Id,Character13_Id,Character14_Id,Character15_Id,Character1Member_Id,Character2Member_Id,Character3Member_Id,Character4Member_Id,Character5Member_Id,Character6Member_Id,Character7Member_Id,Character8Member_Id,Character9Member_Id,Character10Member_Id,Character11Member_Id,Character12Member_Id,Character13Member_Id,Character14Member_Id,Character15Member_Id")] TerritoryPlatoon territoryPlatoon)
        {
            if (ModelState.IsValid)
            {
                territoryPlatoon.Id = Guid.NewGuid();
                db.TerritoryPlatoons.Add(territoryPlatoon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(territoryPlatoon);
        }

        // GET: TerritoryPlatoons/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }
            TerritoryPlatoonModel model = Mapper.Map<TerritoryPlatoon, TerritoryPlatoonModel>(territoryPlatoon);

            ViewBag.Character1_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character1.Character_Id);
            ViewBag.Character2_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character2.Character_Id);
            ViewBag.Character3_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character3.Character_Id);
            ViewBag.Character4_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character4.Character_Id);
            ViewBag.Character5_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character5.Character_Id);
            ViewBag.Character6_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character6.Character_Id);
            ViewBag.Character7_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character7.Character_Id);
            ViewBag.Character8_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character8.Character_Id);
            ViewBag.Character9_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character9.Character_Id);
            ViewBag.Character10_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character10.Character_Id);
            ViewBag.Character11_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character11.Character_Id);
            ViewBag.Character12_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character12.Character_Id);
            ViewBag.Character13_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character13.Character_Id);
            ViewBag.Character14_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character14.Character_Id);
            ViewBag.Character15_Id = new SelectList(db.Characters.OrderBy(x => x.DisplayName), "Id", "DisplayName", model.Character15.Character_Id);            

            return View(territoryPlatoon);
        }

        // POST: TerritoryPlatoons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TerritoryPlatoon territoryPlatoon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territoryPlatoon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(territoryPlatoon);
        }

        // GET: TerritoryPlatoons/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Find(id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }
            return View(territoryPlatoon);
        }

        // POST: TerritoryPlatoons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Find(id);
            db.TerritoryPlatoons.Remove(territoryPlatoon);
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
